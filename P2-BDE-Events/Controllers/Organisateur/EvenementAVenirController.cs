using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.Organisateur
{
    public class EvenementAVenirController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
