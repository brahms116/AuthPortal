namespace AuthLibrary;

public interface IAuth
{
    Task<CreateUserResponse> CreateUser(string email, string username);

    Task<LoginUserResponse> Login(string username, string password);

    Task ChangePassword(string username, string oldPassword, string newPassword);

    Task ForgotPassword(string username);

    Task ConfirmForgotPassword(string username, string newPassword, string confirmationCode);

}
