using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System;
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
        public IActionResult AgendaEvenement(string nomEvent, string universite)
        {
            List<Evenement> evenements = EvenementService.ObtenirTousLesEvenements().Where(e => e.Etat == Models.Evenement.Enums.EtatEvenement.PUBLIE).ToList();

            if (!string.IsNullOrWhiteSpace(nomEvent))
            {
                evenements = evenements.Where(e => e.Titre.Contains(nomEvent, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            int compteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Participant participant = ParticipantService.GetParticipantParCompte(compteId);

           if (!string.IsNullOrEmpty(universite))
            {
                universite = participant.Universite;
                evenements = EvenementService.ObtenirEvenementsParUniversite(universite).Where(e => e.Etat == Models.Evenement.Enums.EtatEvenement.PUBLIE).ToList();

            }
                      
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
