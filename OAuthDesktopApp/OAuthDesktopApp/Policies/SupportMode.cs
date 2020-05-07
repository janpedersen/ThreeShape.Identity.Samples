using ThreeShape.Identity.Policies.Core;

namespace OAuthDesktopApp.Policies
{
    public class SupportMode : AndPolicy
    {
        public SupportMode()
            : base(
                  new RolePolicy("3DD.SupportDesignator"),
                  new RolePolicy("3DD.Supporter"))
        {
        }
    }
}
