using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.AdministrateurControllers
{
    public class DashboardControllercs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
