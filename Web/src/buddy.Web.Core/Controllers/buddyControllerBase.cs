using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace buddy.Controllers
{
    public abstract class buddyControllerBase: AbpController
    {
        protected buddyControllerBase()
        {
            LocalizationSourceName = buddyConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
