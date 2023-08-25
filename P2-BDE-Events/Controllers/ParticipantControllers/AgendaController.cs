using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.ParticipantControllers
{
    public class AgendaController : Controller
    {
        public IActionResult AgendaEvenement()
        {
            return View("Views/Participant/AgendaEvenement.cshtml");
        }
    }
}
