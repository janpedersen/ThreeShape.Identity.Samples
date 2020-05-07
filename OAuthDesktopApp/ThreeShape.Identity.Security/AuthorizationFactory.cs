using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ThreeShape.Identity.Security
{
    public class AuthorizationFactory : IAuthorizationFactory
    {
        public Authorization Create(IEnumerable<Claim> claims, string access_token)
        {
            return Create(new Claims(claims), access_token);
        }

        public Authorization Create(Claims claims, string access_token)
        {
            if (IsClientToken(claims))
            {
                return BuildClientAuthorization(claims, access_token);
            }

            return BuildUserAuthorization(claims, access_token);
        }

        private Authorization BuildClientAuthorization(Claims claims, string access_token)
        {
            return new Authorization
            {
                Token = access_token,
                Name = claims.GetClaimAs<string>("client_id"),
                CompanyId = claims.GetClaimAs<Guid>("companyId"),
                Roles = claims.GetClaimsAs<string>("role").ToList()
            };
        }

        private Authorization BuildUserAuthorization(Claims claims, string access_token)
        {
            return new Authorization
            {
                Token = access_token,
                Id = claims.GetClaimAs<Guid>("sub"),
                CompanyId = claims.GetClaimAs<Guid>("companyId"),
                Name = claims.GetClaimAs<string>("preferred_username"),
                Roles = claims.GetClaimsAs<string>("role").ToList()
            };
        }

        private bool IsClientToken(Claims claims)
        {
            return !claims.Any(x => x.Type == "sub");
        }
    }
}
