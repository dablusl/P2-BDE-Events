using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.Services.Prestations;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class AppelsDoffreController : Controller
    {
        PrestationService prestationService { get; set; }

        public AppelsDoffreController()
        {
            prestationService = new PrestationService();
        }
        public IActionResult ConsulterLesAppels()
        {
            return View("~/Views/Prestation/ConsulterLesAppels.cshtml", ConsultationViewModel());
        }

        public IActionResult ProposerUnePrestation(int idEvenement, int idLigne)
        {
            return View("~/Views/Prestation/ProposerUnePrestation.cshtml", PropositionViewModel(idEvenement, idLigne));
        }
        [HttpPost]
        public IActionResult ProposerUnePrestation(AppelDoffreViewModel viewModel)
        {
            prestationService.CreerPropositionPrestation(viewModel.PropositionPrestation);

            return Redirect("ConsulterLesAppels");
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }
        public AppelDoffreViewModel ConsultationViewModel()
        {
            Prestataire prestataire = new PrestataireService().GetPrestataireParCompte(GetIdCompte());
            List<TypeDePrestation> typesDuPresta = prestataire.Prestations.Select(Prestation => Prestation.Type).ToList();
            List<PropositionPrestation> propositions = prestationService.GetPropositionsDuPrestataire(prestataire.Id);

            //On doit lui passer Evenements ayant dappel de prestation concernants
            AppelDoffreViewModel viewModel = new AppelDoffreViewModel
            {
                PropositionPrestation = new PropositionPrestation(),
                EvenementsEnAppelDoffre = new EvenementService().EnAppelDoffre(typesDuPresta),
                Types = typesDuPresta,
                Propositions = propositions
            };

            return viewModel;
        }

        public AppelDoffreViewModel PropositionViewModel(int idEvenement, int idLigne)
        {
            Prestataire prestataire = new PrestataireService().GetPrestataireParCompte(GetIdCompte());
            List<TypeDePrestation> typesDuPresta = prestataire.Prestations.Select(Prestation => Prestation.Type).ToList();

            Evenement evenement = new EvenementService().ObtenirEvenement(idEvenement);
            LigneEvenement ligne = new LigneEvenementService().ObtenirLigneEvenement(idLigne);

            //On doit lui passer Evenements ayant dappel de prestation concernants
            AppelDoffreViewModel viewModel = new AppelDoffreViewModel
            {
                EvenementInteresse = evenement,
                Ligne = ligne,
                PrestationsDuPrestataire = prestataire.Prestations.Where(p => p.Type == ligne.Type).ToList(),
                PropositionPrestation = new PropositionPrestation(),
            };

            return viewModel;
        }

        public void CreationProposition(AppelDoffreViewModel model)
        {
            


        }
    }
}
