using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;


namespace AwsAuthLibrary;

public class UserRepositoryConfig
{

    public string UserpoolId { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public IAmazonCognitoIdentityProvider Provider { get; set; } = default!;
}
