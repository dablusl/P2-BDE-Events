using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers
{
    public class PrestationController : Controller
    {
        public IActionResult ToutesLesPrestations()
        {
            return View();
        }
    }
}
