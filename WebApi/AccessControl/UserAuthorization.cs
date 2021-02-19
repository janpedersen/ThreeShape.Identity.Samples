using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherWebApi.AccessControl
{
    public class UserAuthorization
    {
        public Guid Id { get; }
        public Guid CompanyId { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }
        public string ClientId { get; }
        public string Ip { get; }

        /// <summary>
        ///  User that is a 3Shape administrator according to 3S ID
        /// </summary>
        public bool Is3ShapeAdministrator => ContainsRole(RoleNames.Company3Shape) && ContainsRole(RoleNames.EmployeeAdministrator);

        /// <summary>
        /// User that is a 3Shape supporter according to 3S ID
        /// </summary>
        public bool Is3ShapeSupport => ContainsRole(RoleNames.Company3Shape) && ContainsRole(RoleNames.EmployeeSupport);

        public UserAuthorization(Guid id, Guid companyId, string email, string clientId, string ip,
            IEnumerable<string> roles = null, bool requireCompany = false)
        {
            Id = id;
            CompanyId = companyId;
            Email = email;
            ClientId = clientId;
            Ip = ip;
            Roles = roles ?? Enumerable.Empty<string>();

            if (requireCompany && !IsLoggedInWithCompany)
            {
                throw new UnauthorizedAccessException("You are required to be logged in with a company to use this api.");
            }
        }

        public bool ContainsRole(string roleName)
        {
            return Roles.Contains(roleName);
        }

        private bool IsLoggedInWithCompany => !CompanyId.Equals(Guid.Empty);
    }
}