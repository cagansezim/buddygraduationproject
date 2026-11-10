using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace buddy.Web.Views
{
    public abstract class buddyRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected buddyRazorPage()
        {
            LocalizationSourceName = buddyConsts.LocalizationSourceName;
        }
    }
}
