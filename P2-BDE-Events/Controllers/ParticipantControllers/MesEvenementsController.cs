using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Linq;
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

            List<Participant> participants = EvenementService.ObtenirParticipants(EvenementID);

            int NbParticipant = participants.Count;

            var CompteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            var participant = ParticipantService.GetParticipantParCompte(CompteId);

            bool dejaReserve = participant.Reservations.Where(r => r.EvenementId == EvenementID).Count() == 1 ? true : false;


            var viewModel = new ReserverUnEvenementViewModel
            {
                Evenement = evenement,
                NbParticipant = NbParticipant,
                DejaReserve = dejaReserve
            };
            

            return View("Views/Participant/ReserverUnEvenement.cshtml", viewModel);
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

            var CompteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            var participant = ParticipantService.GetParticipantParCompte(CompteId);


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

        [HttpGet]
        public IActionResult MesEvenementsPart()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login/Index");
            }
            int compteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Participant participant = new ParticipantService().GetParticipantParCompte(compteId);

            var evenements = EvenementService.ObtenirEvenementsReservesParParticipant(participant.Id);

            var viewModel = new MesEvenementsPartViewModel
            {
                Evenements = evenements
            };

            return View("Views/Participant/MesEvenementsPart.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult EvenementReserve(int EvenementID)
        {
            var evenement = EvenementService.ObtenirEvenement(EvenementID);

            List<Participant> participants = EvenementService.ObtenirParticipants(EvenementID);

            int NbParticipant = participants.Count;

            var viewModel = new ReserverUnEvenementViewModel
            {
                Evenement = evenement,
                NbParticipant = NbParticipant
            };


            return View("Views/Participant/EvenementReserve.cshtml", viewModel);
        }



    }
}
