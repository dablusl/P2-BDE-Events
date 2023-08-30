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
    public class AgendaController : Controller
    {
        private EvenementService EvenementService;
        private ParticipantService ParticipantService;

        public AgendaController()
        {
            EvenementService = new EvenementService();
            ParticipantService = new ParticipantService();
        }

        [HttpGet]
        public IActionResult AgendaEvenement(int? eventId, string universite)
        {
            List<Evenement> evenements = EvenementService.ObtenirTousLesEvenements();

            if (eventId.HasValue)
            {
                evenements = evenements.Where(e => e.Id == eventId.Value).ToList();
            }
            int compteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Participant participant = ParticipantService.GetParticipantParCompte(compteId);

           if (!string.IsNullOrEmpty(universite))
            {
                universite = participant.Universite;
                evenements = EvenementService.ObtenirEvenementsParUniversite(universite);

            }
            
            //List<Participant> listeParticipants = new List<Participant>();
            //int NbParticipant = 0;
            //foreach (var evenement in evenements)
            //{
            //    listeParticipants = EvenementService.ObtenirParticipants(evenement.Id);
            //    NbParticipant = listeParticipants.Count;
            //}


            
            var viewModel = new AgendaEvenementViewModel
            {
                Evenements = evenements,
                Participant = participant,
                //Participants = listeParticipants,
                //NbParticipant = NbParticipant

            };

            return View("Views/Participant/AgendaEvenement.cshtml", viewModel);
        }

    }
}
