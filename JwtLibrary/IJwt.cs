using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace JwtLibrary;

public interface IJwt
{
    Task<string> GenerateToken(IList<Claim> claims);

}
