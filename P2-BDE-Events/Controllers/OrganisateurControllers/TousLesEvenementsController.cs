using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class TousLesEvenementsController : Controller
    {


        private EvenementService EvenementService;
        private OrganisateurService OrganisateurService;

        public TousLesEvenementsController()
        {
            EvenementService = new EvenementService();
            OrganisateurService = new OrganisateurService();
        }

        [HttpGet]
        public IActionResult TousLesEvenement(string nomEvent, string universite)
        {
            List<Evenement> evenements = EvenementService.ObtenirTousLesEvenements().Where(e =>e.Etat == Models.Evenement.Enums.EtatEvenement.PUBLIE).ToList();

            if (!string.IsNullOrWhiteSpace(nomEvent))
            {
                evenements = evenements.Where(e => e.Titre.Contains(nomEvent, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            int compteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Organisateur organisateur = OrganisateurService.GetOrganisateurParCompte(compteId);

            if (!string.IsNullOrEmpty(universite))
            {
                universite = organisateur.Participant.Universite;
                evenements = EvenementService.ObtenirEvenementsParUniversite(universite);

            }
            
            var viewModel = new TousLesEvenementsViewModel
            {
                Evenements = evenements,
                Organisateur = organisateur
            };

            return View("Views/Organisateur/TousLesEvenements.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult SolliciterEvenement(int EvenementID)
        {
            var evenement = EvenementService.ObtenirEvenement(EvenementID);

            List<Participant> participants = EvenementService.ObtenirParticipants(EvenementID);

            int NbParticipant = participants.Count;

            var viewModel = new ReserverUnEvenementViewModel
            {
                Evenement = evenement,
                NbParticipant = NbParticipant
            };


            return View("Views/Organisateur/SolliciterEvenement.cshtml", viewModel);
        }

    }

}
