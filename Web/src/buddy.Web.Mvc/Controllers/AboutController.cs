using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using buddy.Controllers;

namespace buddy.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : buddyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
