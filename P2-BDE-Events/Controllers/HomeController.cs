using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Organisateur")]
        public IActionResult testaccesprofil()
        {
            return View();
        }

    }
}
