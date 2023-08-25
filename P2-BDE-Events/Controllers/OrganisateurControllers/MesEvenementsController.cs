using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class MesEvenementsController : Controller
    {
        public IActionResult EvenementsEnCours()
        {
            return View("Views/Organisateur/EvenementsEnCours.cshtml");
        }


        public IActionResult EvenementsPrecedents()
        {
            return View("Views/Organisateur/EvenementsPrecedents.cshtml");
        }

        public IActionResult EvenementsSuivants()
        {
            return View("Views/Organisateur/EvenementsSuivants.cshtml");
        }
    }
}
