using Abp.AspNetCore.Mvc.Authorization;
using buddy.Controllers;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace reka.ablantemizler.Web.Controllers;

[AllowAnonymous]
public class PublicController : buddyControllerBase
{
    public IActionResult Index()
    {
        return Redirect("/Public/index.html");
    }
    public ActionResult Dashboard()
    {
        return Redirect("/Public/dashboard.html");
    }
    public IActionResult About()
    {
        return Redirect("/Public/about.html");
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Login()
    {
        return RedirectToAction("Login", "Account");
    }
}