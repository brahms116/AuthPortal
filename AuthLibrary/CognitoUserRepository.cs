using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using System.Security.Cryptography;
using System.Text;

namespace AuthLibrary
{
	public class CognitoUserRepository : IAuth
	{

		private readonly IAmazonCognitoIdentityProvider _provider;
		private readonly string _userpoolId;
		private readonly string _clientId;
		private readonly string _clientSecret;


		private string GetSecretHash(string username)
		{
			var hash = new HMACSHA256(Encoding.UTF8.GetBytes(_clientSecret));

			var signature = hash.ComputeHash(Encoding.UTF8.GetBytes(username + _clientId));

			return Convert.ToBase64String(signature);
		}

		public CognitoUserRepository(CognitoUserRepositoryConfig config)
		{
			if (config is null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			_provider = config.Provider ?? throw new ArgumentNullException(nameof(config.Provider));
			_userpoolId = config.UserpoolId ?? throw new ArgumentNullException(nameof(config.UserpoolId));
			_clientId = config.ClientId ?? throw new ArgumentNullException(nameof(config.ClientId));
			_clientSecret = config.ClientSecret ?? throw new ArgumentNullException(nameof(config.ClientSecret));
		}

		public async Task<CreateUserResponse> CreateUser(string email, string username)
		{
			if (string.IsNullOrEmpty(email))
			{
				throw new ArgumentException($"'{nameof(email)}' cannot be null or empty.", nameof(email));
			}

			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
			}

			var config = new AdminCreateUserRequest
			{
				Username = username,
				UserAttributes = new List<AttributeType>() {
					new AttributeType(){ Name="email", Value=email }
				},
				UserPoolId = _userpoolId
			};

			try
			{
				var result = await _provider.AdminCreateUserAsync(config);
				return new CreateUserResponse()
				{
					Username = result.User.Username,
					UserStatus = result.User.UserStatus,
					UserCreateDate = new DateTimeOffset(result.User.UserCreateDate).ToUnixTimeSeconds()
				};
			}
			catch (Exception ex)
			{
				throw new SignUpFailureException("Failed to sign up", ex);
			}
		}

		public async Task<LoginUserResponse> Login(string username, string password)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
			}

			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
			}



			var request = new AdminInitiateAuthRequest()
			{
				AuthFlow = AuthFlowType.ADMIN_USER_PASSWORD_AUTH,
				AuthParameters = new Dictionary<string, string>()
				{
					["USERNAME"] = username,
					["PASSWORD"] = password,
					["SECRET_HASH"] = GetSecretHash(username)
				},
				ClientId = _clientId,
				UserPoolId = _userpoolId,

			};


			try
			{

				var result = await _provider.AdminInitiateAuthAsync(request);

				if (result.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
				{
					throw new NewPasswordNeededException();
				}

				if (result.AuthenticationResult == null)
				{
					throw new UnknownAuthChallengeException(result.ChallengeName.ToString());
				}

				var updateRequest = new AdminUpdateUserAttributesRequest()
				{
					UserAttributes = new List<AttributeType>() {
						new AttributeType() { Name="email_verified", Value="true"}
					},
					Username = username,
					UserPoolId = _userpoolId,
				};

				await _provider.AdminUpdateUserAttributesAsync(updateRequest);

				return new LoginUserResponse()
				{
					Token = result.AuthenticationResult.AccessToken,
					RefreshToken = result.AuthenticationResult.RefreshToken,
				};
			}
			catch (NewPasswordNeededException)
			{
				throw;
			}
			catch (NotAuthorizedException ex)
			{
				throw new IncorrectCredentialsException("Incorrect Creds", ex);
			}
			catch (UserNotFoundException ex)
			{
				throw new IncorrectUsernameException("Incorrect Username", ex);
			}
			catch (Exception ex)
			{

				throw new LoginFailureException("Failed to login.", ex);
			}

		}


		public async Task ChangePassword(string username, string oldPassword, string newPassword)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
			}

			if (string.IsNullOrEmpty(oldPassword))
			{
				throw new ArgumentException($"'{nameof(oldPassword)}' cannot be null or empty.", nameof(oldPassword));
			}

			if (string.IsNullOrEmpty(newPassword))
			{
				throw new ArgumentException($"'{nameof(newPassword)}' cannot be null or empty.", nameof(newPassword));
			}

			try
			{
				var loginResult = await this.Login(username, oldPassword);
				if (string.IsNullOrEmpty(loginResult.Token))
				{

					throw new UnknownException();
				}
			}
			catch (NewPasswordNeededException) { };



			var request = new AdminSetUserPasswordRequest()
			{
				Password = newPassword,
				Permanent = true,
				Username = username,
				UserPoolId = _userpoolId

			};

			try
			{
				await _provider.AdminSetUserPasswordAsync(request);
			}
			catch (InvalidPasswordException ex)
			{
				throw new PasswordValidationException("Password failed to meet constraints", ex);
			}
			catch (Exception ex)
			{
				throw new ChangePasswordFailureException("Failed to change password.", ex);
			}

		}

		public async Task ForgotPassword(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
			}

			var request = new ForgotPasswordRequest()
			{
				ClientId = _clientId,
				Username = username,
				SecretHash = GetSecretHash(username)
			};

			try
			{
				await _provider.ForgotPasswordAsync(request);
			}
			catch (Exception ex)
			{

				throw new ForgotPasswordFailureException("Failed to forget password.", ex);
			}
		}

		public async Task ConfirmForgotPassword(string username, string newPassword, string confirmationCode)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
			}

			if (string.IsNullOrEmpty(newPassword))
			{
				throw new ArgumentException($"'{nameof(newPassword)}' cannot be null or empty.", nameof(newPassword));
			}

			if (string.IsNullOrEmpty(confirmationCode))
			{
				throw new ArgumentException($"'{nameof(confirmationCode)}' cannot be null or empty.", nameof(confirmationCode));
			}

			var request = new ConfirmForgotPasswordRequest()
			{
				ClientId = _clientId,
				Username = username,
				SecretHash = GetSecretHash(username),
				ConfirmationCode = confirmationCode,
				Password = newPassword
			};

			try
			{
				await _provider.ConfirmForgotPasswordAsync(request);
			}

			catch (InvalidPasswordException ex)
			{
				throw new PasswordValidationException("Password failed to meet constraints", ex);
			}

			catch (Exception ex)
			{
				throw new ConfirmForgotPasswordFailureException("Failed to confirm forgot password.", ex);
			}

		}

	}
}