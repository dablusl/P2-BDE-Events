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
        private EvenementService evenementService;
        public PropositionsPrestationsController()
        {
            ligneEvenementService = new LigneEvenementService();
            prestationService = new PrestationService();
            evenementService = new EvenementService();
        }

        public IActionResult PrestationsDelEvenement(int id)
        {
            List<LigneEvenement> lignes = ligneEvenementService.GetLignesEvenement(id);

            return View("~/Views/Organisateur/PrestationsDelEvenement.cshtml",lignes);
        }

        public IActionResult PropositionsDeLaPrestation(int id)
        {
            return View("~/Views/Organisateur/PropositionsPrestations.cshtml",InitViewModel(id));
        }

        [HttpPost]
        public IActionResult PropositionsDeLaPrestation(PropositionsPrestationViewModel model)
        {
            // authoriser seulement a lorganisateur de levenement
            ligneEvenementService.ChoisirPrestation(model.LigneId,model.PropositionID);
            prestationService.NettoyerPropositions(model.LigneId);

            return RedirectToAction("PrestationsDelEvenement",new { id = model.EvenementID });
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
                LigneId = idLigne,
                EvenementID = ligne.EvenementId
            };
             
            return viewModel;
        }

    }
}
