using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.AdministrateurControllers
{
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View("~/Views/Admin/Index.cshtml");
        }
    }
}
