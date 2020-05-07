using ThreeShape.Identity.Policies.Core;

namespace OAuthDesktopApp.Policies
{
    public class HasAccessTo3DDSupportMode : AndPolicy
    {
        //if user is Reseller or 3Shape
        //and if user has both 3DD.SupportDesignator and 3DD.Supporter
        //we have included both roles SupportDesignator and Supporter so it would be easy to take away this permission from a company again
        // // 3DD.Supporter can be assigned multiple places in a company, making it harder to cleanup if the permission has to be removed again
        public HasAccessTo3DDSupportMode()
            : base(
                  //This policy could have been a special policy, instantiated the same way as support mode
                  new OrPolicy(
                      new RolePolicy("Company.3Shape"),
                      new RolePolicy("Company.Reseller")
                      ),
                  new SupportMode()
                  )
        {
        }
    }
}
