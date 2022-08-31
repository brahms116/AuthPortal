using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary;

public class CognitoUserServiceConfig
{
    public string? ClientID { get; set; }
    public string? ClientSecret { get; set; }
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }
    public string? UserpoolId { get; set; }

}
