using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;


namespace AuthLibrary;

public class CognitoUserRepositoryConfig
{

    public string? UserpoolId { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public IAmazonCognitoIdentityProvider? Provider { get; set; }
}
