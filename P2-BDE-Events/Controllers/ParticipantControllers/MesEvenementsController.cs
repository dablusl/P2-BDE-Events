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

        //[HttpGet]
        //public IActionResult ReserverUnEvenement()
        //{
        //    return View("Views/Participant/ReserverUnEvenement.cshtml");
        //}
        [HttpGet]
        public IActionResult ReserverUnEvenement(int EvenementID)
        {
            var evenement = EvenementService.ObtenirEvenement(EvenementID);

            //if (evenement == null)
            //{
            //    return NotFound(); // L'événement n'a pas été trouvé
            //}

            return View("Views/Participant/ReserverUnEvenement.cshtml", evenement);
        }

        [HttpPost]
        [ActionName("ReserverUnEvenement")]
        public IActionResult ReserverUnEvenementPost(int evenementId)
        {
            // Assurez-vous que l'utilisateur est authentifié en tant que participant
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("/Login/Index"); // Rediriger vers la page de connexion si non authentifié
            }

            // Récupérer l'événement à partir de la base de données
            var evenement = EvenementService.ObtenirEvenement(evenementId);

            //if (evenement == null)
            //{
            //    return NotFound(); // L'événement n'a pas été trouvé
            //}

            // Récupérer l'utilisateur actuellement connecté (participant)
            var utilisateurId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            var participant = ParticipantService.ObtenirParticipant(utilisateurId);

            //if (participant == null)
            //{
            //    return NotFound(); // Le participant n'a pas été trouvé
            //}

            // Ajouter la réservation pour l'événement et le participant
            EvenementService.ReserverEvenement(participant.Id, evenement.Id);

            // Rediriger vers une page de confirmation
            TempData["EvenementTitre"] = evenement.Titre;

            return RedirectToAction("Confirmation");
        }
        public IActionResult Confirmation()
        {
            var evenementTitre = TempData["EvenementTitre"] as string;

            // réinitialiser TempData pour libérer la mémoire
            TempData.Remove("EvenementTitre");

            ViewBag.EvenementTitre = evenementTitre;
            return View("Views/Participant/Confirmation.cshtml");
        }

    }
}
