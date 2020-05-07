using System.Collections.Generic;
using System.Security.Claims;

namespace ThreeShape.Identity.Security
{
    public interface IAuthorizationFactory
    {
        Authorization Create(IEnumerable<Claim> claims, string access_token = null);
        Authorization Create(Claims claims, string access_token = null);
    }
}
