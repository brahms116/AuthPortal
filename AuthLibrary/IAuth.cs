namespace AuthLibrary;

public interface IAuth
{
    public Task<CreateUserResponse> CreateUser(string email, string username);

    public Task<LoginUserResponse> Login(string username, string password);

    public Task ChangePassword(string username, string oldPassword, string newPassword);

    public Task ForgotPassword(string username);

    public Task ConfirmForgotPassword(string username, string newPassword, string confirmationCode);

}
