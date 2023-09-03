using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class MesEvenementsController : Controller
    {
        private EvenementService EvenementService;
        private OrganisateurService OrganisateurService;
        private ParticipantService ParticipantService;

        public MesEvenementsController()
        {
            EvenementService = new EvenementService();
            OrganisateurService = new OrganisateurService();
            ParticipantService = new ParticipantService();
        }

        //public IActionResult MesEvenementsOrgaTermines()
        //{
        //    return View("Views/Organisateur/MesEvenementsOrgaTermines.cshtml");
        //}

        [HttpGet]
        public IActionResult ConsulterParticipants(int evenementId)
        {
            var evenement = EvenementService.ObtenirEvenement(evenementId);
            List<Participant> participants = EvenementService.ObtenirParticipants(evenementId);

            var viewModel = new ConsulterEvenementViewModel
            {
                Evenement = evenement,
                Participants = participants
            };

            return View("Views/Organisateur/ConsulterParticipants.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult ConsulterEvenement(int evenementId)
        {
            var evenement = EvenementService.ObtenirEvenement(evenementId);
            List<Participant> participants = EvenementService.ObtenirParticipants(evenementId);

            int NbParticipant = participants.Count;

            var viewModel = new ConsulterEvenementViewModel
            {
                Evenement = evenement,
                Participants = participants,
                NbParticipant = NbParticipant
            };

            return View("Views/Organisateur/ConsulterEvenement.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult MesEvenementsOrga()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login/Index");
            }
            int compteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Organisateur organisateur = new OrganisateurService().GetOrganisateurParCompte(compteId);

            // Récupérer la liste des événements créés par l'organisateur
            var evenements = EvenementService.ObtenirEvenementsOrganisateur(organisateur.Id).Where(e => e.Etat != Models.Evenement.Enums.EtatEvenement.PASSE).ToList();

            var viewModel = new MesEvenementsOrgaViewModel
            {
                Evenements = evenements
            };

            return View("Views/Organisateur/MesEvenementsOrga.cshtml", viewModel);
        }
        [HttpGet]
        public IActionResult MesEvenementsOrgaTermines()
        {
            
            int compteId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Organisateur organisateur = new OrganisateurService().GetOrganisateurParCompte(compteId);

            var evenements = EvenementService.ObtenirEvenementsOrganisateur(organisateur.Id).Where(e => e.Etat == Models.Evenement.Enums.EtatEvenement.PASSE).ToList();
    
            var viewModel = new MesEvenementsOrgaViewModel
            {
                Evenements = evenements
            };

            return View("Views/Organisateur/MesEvenementsOrgaTermines.cshtml", viewModel);
        }


    }
}
