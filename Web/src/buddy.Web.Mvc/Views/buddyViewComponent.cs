using Abp.AspNetCore.Mvc.ViewComponents;

namespace buddy.Web.Views
{
    public abstract class buddyViewComponent : AbpViewComponent
    {
        protected buddyViewComponent()
        {
            LocalizationSourceName = buddyConsts.LocalizationSourceName;
        }
    }
}
