using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace WeatherWebApi.AccessControl
{
    public class UserAuthorizationFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly bool _requireCompany;

        public UserAuthorizationFactory(IHttpContextAccessor httpContextAccessor, IOptions<IdentityConfig> identityConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _requireCompany = identityConfig.Value?.RequireCompany ?? false;
        }

        public UserAuthorization GetAuthorizedUser()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            var id = GetFromClaims<Guid>(claims, "sub");
            var email = GetFromClaims<string>(claims, "email");
            var companyId = GetFromClaims<Guid>(claims, "companyId");
            var clientId = GetFromClaims<string>(claims, "client_id");
            var roles = GetListFromClaims<string>(claims, "role");
            var userIp = GetClientIp(claims);

            return new UserAuthorization(id, companyId, email, clientId, userIp, roles, _requireCompany);
        }

        private List<T> GetListFromClaims<T>(IEnumerable<Claim> claims, string name)
        {
            var claimValues = new List<T>();
            var claimsList = claims.Where(x => x.Type == name).ToList();

            foreach (var claim in claimsList)
            {
                var claimValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(claim.Value);
                claimValues.Add(claimValue);
            }

            return claimValues;
        }

        private T GetFromClaims<T>(IEnumerable<Claim> claims, string name)
        {
            var claim = claims.FirstOrDefault(x => x.Type == name);

            if (claim != null)
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
            }

            return default;
        }

        private string GetClientIp(List<Claim> claims)
        {
            var userIp = GetFromClaims<string>(claims, "user_ip");
            if (userIp != null)
            {
                return userIp;
            }

            if (_httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress == null)
            {
                return string.Empty;
            }

            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            return ip.IsIPv4MappedToIPv6 ? ip.MapToIPv4().ToString() : ip.ToString();
        }
    }
}