using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLibrary
{

    public class CreateUserResponse {
        public string? Username { get; set; } 
        public string? Email { get; set; } 
        public string? UserStatus { get; set; } 
        public long? UserCreateDate { get; set; } 
    }


    public class LoginUserResponse {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }


    
}
