using System.Text.Json.Serialization;

namespace AuthPortalApi;


public class LoginAto
{

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }



    public LoginAto(string username, string password)
    {
        Username = username;
        Password = password;
    }


}

public class LoginResponseAto
{

    [JsonPropertyName("token")]
    public string Token { get; set; }

    public LoginResponseAto(string Token)
    {
        this.Token = Token;
    }
}
public class TokenResponseAto
{

    [JsonPropertyName("token")]
    public string Token { get; set; }

    public TokenResponseAto(string Token)
    {
        this.Token = Token;
    }
}

public class SignupAto
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    public SignupAto(string username, string email)
    {
        Username = username;
        Email = email;
    }
}

public class SignupResponseAto {

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("userStatus")]
    public string UserStatus { get; set; }

    [JsonPropertyName("userCreateDate")]
    public long UserCreateDate { get; set; }

    public SignupResponseAto(string username, string email, string userStatus, long userCreateDate)
    {
        Username = username;
        Email = email;
        UserStatus = userStatus;
        UserCreateDate = userCreateDate;
    }
}

public class ForgotPasswordAto {

    [JsonPropertyName("username")]
    public string Username { get; set; }

    public ForgotPasswordAto(string username)
    {
        Username = username;
    }
}
public class ForgotPasswordResponseAto {

    [JsonPropertyName("username")]
    public string Username { get; set; }

    public ForgotPasswordResponseAto(string username)
    {
        Username = username;
    }
}

public class ChangePasswordAto { 

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("oldPassword")]
    public string OldPassword { get; set; }

    [JsonPropertyName("newPassword")]
    public string NewPassword { get; set; }

    public ChangePasswordAto(string username, string newPassword, string oldPassword)
    {
        Username = username;
        NewPassword = newPassword;
        OldPassword = oldPassword;
    }
}

public class ChangePasswordResponseAto { 

    [JsonPropertyName("username")]
    public string Username { get; set; }

    public ChangePasswordResponseAto(string username)
    {
        Username = username;
    }
}

public class ConfirmForgotPasswordAto
{

    [JsonPropertyName("username")]
    public string Username { get; set; }


    [JsonPropertyName("newPassword")]
    public string NewPassword { get; set; }

    [JsonPropertyName("confirmationCode")]
    public string ConfirmationCode { get; set; }

    public ConfirmForgotPasswordAto(string confirmationCode, string newPassword, string username)
    {
        ConfirmationCode = confirmationCode;
        NewPassword = newPassword;
        Username = username;
    }
}

public class ConfirmForgotPasswordResponseAto { 

    [JsonPropertyName("username")]
    public string Username { get; set; }

    public ConfirmForgotPasswordResponseAto(string username)
    {
        Username = username;
    }
}
