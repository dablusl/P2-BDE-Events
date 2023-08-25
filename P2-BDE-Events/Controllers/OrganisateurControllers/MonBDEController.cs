using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class MonBDEController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
