using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.Services.Prestations;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class PropositionsPrestationsController : Controller
    {
        private LigneEvenementService ligneEvenementService;
        private PrestationService prestationService;
        public PropositionsPrestationsController()
        {
            ligneEvenementService = new LigneEvenementService();
            prestationService = new PrestationService();
        }

        public IActionResult PropositionsDeLaPrestation(int id)
        {
            // authoriser seulement a lorganisateur de levenement

            return View("~/Views/Organisateur/PropositionsPrestations.cshtml",InitViewModel(id));
        }

        [HttpPost]
        public IActionResult PropositionsDeLaPrestation(PropositionsPrestationViewModel model)
        {
            // authoriser seulement a lorganisateur de levenement
            ligneEvenementService.ChoisirPrestation(model.Ligne.Id,model.PrestationChoisi,0);
            prestationService.NettoyerPropositions(model.Ligne.Id);

            return View();
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
                Propositions = propositions,
                PrestationChoisi = new Prestation()
            };
             
            return viewModel;
        }

    }
}
