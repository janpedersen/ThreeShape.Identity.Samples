using System;
using System.Collections.Generic;
using System.Linq;

namespace ThreeShape.Identity.Security
{
    public class Authorization
    {
        public string Token { get; set; }

        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

        public bool ContainsRole(string roleName)
        {
            return Roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
