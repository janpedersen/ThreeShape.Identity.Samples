using System.Collections.Generic;
using System.Linq;
using ThreeShape.Identity.Security;

namespace ThreeShape.Identity.Policies.Core
{
    public class AndPolicy : Policy
    {
        private readonly List<Policy> policies;

        public AndPolicy(params Policy[] policies)
        {
            this.policies = policies.ToList();
        }

        public override bool IsValid(Authorization authorization)
        {
            return policies.All(p => p.IsValid(authorization));
        }
    }
}
