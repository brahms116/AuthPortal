using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtLibrary;

public class JwtService : IJwt

{
    private readonly TimeSpan _timeTillExpire;
    private readonly string _secret;

    public JwtService(TimeSpan timeTillExpire, string secret)
    {
        _timeTillExpire = timeTillExpire;
        _secret = secret;
    }

    public Task<string> GenerateToken(IList<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.Add(_timeTillExpire),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(jwt);
    }
}
