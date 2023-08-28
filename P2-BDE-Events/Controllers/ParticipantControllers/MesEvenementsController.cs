using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.ParticipantControllers
{
    public class MesEvenementsController : Controller
    {
        public IActionResult ProchainsEvenements()
        {
            return View("Views/Participant/ProchainsEvenements.cshtml");
        }


        public IActionResult EvenementsTermines()
        {
            return View("Views/Participant/EvenementsTermines.cshtml");
        }

        public IActionResult EvenementsInteressant()
        {
            return View("Views/Participant/EvenementsInteressant.cshtml");
        }

        public IActionResult ReserverUnEvenement()
        {

        }
    }
}
