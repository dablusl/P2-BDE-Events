using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.Organisateur
{
    public class MonBDEController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
