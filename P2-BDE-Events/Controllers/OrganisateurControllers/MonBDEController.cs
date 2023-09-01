using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class MonBDEController : Controller
    {
        public IActionResult Compte()
        {
            return View("/Views/Organisateur/Compte.cshtml");
        }
    }
}
