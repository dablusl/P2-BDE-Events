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
            return View("~/Views/Organisateur/PrestationsDelEvenement.cshtml",PrestationViewModel(id));
        }

        public IActionResult PropositionsDeLaPrestation(int id)
        {
            return View("~/Views/Organisateur/PropositionsPrestations.cshtml",PropositionViewModel(id));
        }

        [HttpPost]
        public IActionResult PropositionsDeLaPrestation(PropositionsPrestationViewModel model)
        {
            ligneEvenementService.ChoisirPrestation(model.LigneId,model.PropositionID,model.EvenementID);
            evenementService.PublierEvenement(model.EvenementID);
            prestationService.NettoyerPropositions(model.LigneId);

            return RedirectToAction("PrestationsDelEvenement",new { id = model.EvenementID });
        }


        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }

        public PropositionsPrestationViewModel PropositionViewModel(int idLigne)
        {
            LigneEvenement ligne = ligneEvenementService.ObtenirLigneEvenement(idLigne);
            List<PropositionPrestation> propositions = ligneEvenementService.ObtenirPropositions(idLigne);

            return new PropositionsPrestationViewModel
            {
                Ligne = ligne,
                Propositions = propositions,
                Evenement = evenementService.ObtenirEvenement(ligne.EvenementId),
                LigneId = idLigne,
                EvenementID = ligne.EvenementId
            };
        }

        public PrestationEvenementViewModel PrestationViewModel(int idEvenement)
        {
            return new PrestationEvenementViewModel
            {
                Lignes = ligneEvenementService.GetLignesEvenement(idEvenement),
                Evenement = evenementService.ObtenirEvenement(idEvenement),
            };
        }

    }
}
