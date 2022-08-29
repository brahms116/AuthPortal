using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

namespace AwsAuthLibrary
{
	public class UserRepository
	{

		private readonly IAmazonCognitoIdentityProvider _provider;

		public UserRepository(IAmazonCognitoIdentityProvider provider)
		{
			this._provider = provider;
		}

		public async void CreateUser(string email, string username)
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
					new AttributeType(){ Name="email", Value=email}
				}
			};

			var result = await _provider.AdminCreateUserAsync(config);



		}
	}
}