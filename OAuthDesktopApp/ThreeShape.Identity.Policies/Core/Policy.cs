using ThreeShape.Identity.Security;

namespace ThreeShape.Identity.Policies.Core
{
    public abstract class Policy
    {
        public abstract bool IsValid(Authorization authorization);
    }
}
