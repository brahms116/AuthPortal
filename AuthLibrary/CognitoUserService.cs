using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;

namespace AuthLibrary;

public class CognitoUserService: IAuth
{


    private readonly CognitoUserRepository _userRepository;

    public CognitoUserService(CognitoUserServiceConfig config) {
        if (config is null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        var clientId = config.ClientID ?? throw new ArgumentNullException(nameof(config.ClientID));
        var userpoolId = config.UserpoolId ?? throw new ArgumentNullException(nameof(config.UserpoolId));
        var secretAccessKey = config.SecretAccessKey ?? throw new ArgumentNullException(nameof(config.SecretAccessKey));
        var accessKeyId = config.AccessKeyId ?? throw new ArgumentNullException(nameof(config.AccessKeyId));
        var clientSecret = config.ClientSecret ?? throw new ArgumentNullException(nameof(config.ClientSecret));

        var provider = new AmazonCognitoIdentityProviderClient(accessKeyId, secretAccessKey);
        

        _userRepository = new CognitoUserRepository(new CognitoUserRepositoryConfig()
        {
            Provider = provider,
            ClientId = clientId,
            UserpoolId = userpoolId,
            ClientSecret = clientSecret
        });

    }

    public async Task ChangePassword(string username, string oldPassword, string newPassword)
    {
        await _userRepository.ChangePassword(username, oldPassword, newPassword);
    }

    public async Task ConfirmForgotPassword(string username, string newPassword, string confirmationCode)
    {
        await _userRepository.ConfirmForgotPassword(username, newPassword, confirmationCode);
    }

    public async Task<CreateUserResponse> CreateUser(string email, string username)
    {
        return await _userRepository.CreateUser(email, username);
    }

    public async Task ForgotPassword(string username)
    {
        await _userRepository.ForgotPassword(username);
    }

    public async Task<LoginUserResponse> Login(string username, string password)
    {
       return await _userRepository.Login(username, password);
    }
}
