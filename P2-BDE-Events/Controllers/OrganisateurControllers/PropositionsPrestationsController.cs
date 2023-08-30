using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class PropositionsPrestationsController : Controller
    {
        private LigneEvenementService ligneEvenementService;
        public PropositionsPrestationsController()
        {
            ligneEvenementService = new LigneEvenementService();
        }

        public IActionResult PropositionsDeLaPrestation(int id)
        {
            // authoriser seulement a lorganisateur de levenement

            return View("~/Views/Organisateur/PropositionsPrestations.cshtml",InitViewModel(id));
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }

        public PropositionsPrestationViewModel InitViewModel(int idLigne)
        {
            LigneEvenement ligne = ligneEvenementService.ObtenirLigneEvenement(idLigne);
            List<PropositionPrestation> propositions = ligneEvenementService.ObtenirPropositions(idLigne);

            PropositionsPrestationViewModel viewModel = new PropositionsPrestationViewModel
            {
                Ligne = ligne,
                Propositions = propositions
            };
             
            return viewModel;
        }

    }
}
