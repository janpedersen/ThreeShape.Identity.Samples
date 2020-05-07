using ThreeShape.Identity.Security;
using System.Collections.Generic;
using System.Linq;

namespace ThreeShape.Identity.Policies.Core
{
    public class OrPolicy : Policy
    {
        private readonly List<Policy> policies;

        public OrPolicy(params Policy[] policies)
        {
            this.policies = policies.ToList();
        }

        public override bool IsValid(Authorization authorization)
        {
            return policies.Any(p => p.IsValid(authorization));
        }
    }
}
