using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Security.Claims;

namespace P2_BDE_Events.Controllers.ParticipantControllers
{
    public class MesEvenementsController : Controller
    {
        private EvenementService EvenementService { get; set; }
        private ParticipantService ParticipantService { get; set; }
        public MesEvenementsController()
        {
            EvenementService = new EvenementService();
            ParticipantService = new ParticipantService();
        }
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


        [HttpGet]
        public IActionResult ReserverUnEvenement(int EvenementID)
        {
            var evenement = EvenementService.ObtenirEvenement(EvenementID);

            return View("Views/Participant/ReserverUnEvenement.cshtml", evenement);
        }

        [HttpPost]
        [ActionName("ReserverUnEvenement")]
        public IActionResult ReserverUnEvenementPost(int evenementId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("/Login/Index");
            }

            var evenement = EvenementService.ObtenirEvenement(evenementId);

            var utilisateurId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            var participant = ParticipantService.ObtenirParticipant(utilisateurId);


            EvenementService.ReserverEvenement(participant.Id, evenement.Id);

            TempData["EvenementTitre"] = evenement.Titre;

            return RedirectToAction("Confirmation");
        }
        public IActionResult Confirmation()
        {
            var evenementTitre = TempData["EvenementTitre"] as string;

            TempData.Remove("EvenementTitre");

            ViewBag.EvenementTitre = evenementTitre;
            return View("Views/Participant/Confirmation.cshtml");
        }

    }
}
