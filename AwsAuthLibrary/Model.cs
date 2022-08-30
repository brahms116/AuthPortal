using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsAuthLibrary
{

    public class CreateUserResponse {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string UserStatus { get; set; } = default!;
        public long UserCreateDate { get; set; } = default!;
    }


    public class LoginUserResponse {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }


    
}
