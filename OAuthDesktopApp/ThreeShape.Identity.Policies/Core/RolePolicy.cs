using ThreeShape.Identity.Security;

namespace ThreeShape.Identity.Policies.Core
{
    public class RolePolicy : Policy
    {
        private readonly string role;

        public RolePolicy(string role)
        {
            this.role = role;
        }

        public override bool IsValid(Authorization authorization)
        {
            return authorization.ContainsRole(role);                
        }
    }
}
