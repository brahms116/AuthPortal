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
