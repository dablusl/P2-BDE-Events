using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class AppelsDoffreController : Controller
    {
        //afficher toutes les appels d'offre possibles par prestations du prestataire
        //commentaire test push
        public IActionResult ConsulterLesAppels()
        {
            return View("~/Views/Prestation/ConsulterLesAppels.cshtml", ConsultationViewModel());
        }

        public IActionResult ProposerUnePrestation(int idEvenement, TypeDePrestation type)
        {
            return View("~/Views/Prestation/ProposerUnePrestation.cshtml", PropositionViewModel(idEvenement, type));
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }
        public AppelDoffreViewModel ConsultationViewModel()
        {
            Prestataire prestataire = new PrestataireService().GetPrestataireParCompte(GetIdCompte());
            List<TypeDePrestation> typesDuPresta = prestataire.Prestations.Select(Prestation => Prestation.Type).ToList();

            //On doit lui passer Evenements ayant dappel de prestation concernants
            AppelDoffreViewModel viewModel = new AppelDoffreViewModel
            {
                PropositionPrestation = new PropositionPrestation(),
                EvenementsEnAppelDoffre = new EvenementService().EnAppelDoffre(typesDuPresta),
                Types = typesDuPresta,
            };

            return viewModel;
        }

        public AppelDoffreViewModel PropositionViewModel(int idEvenement, TypeDePrestation type)
        {
            Prestataire prestataire = new PrestataireService().GetPrestataireParCompte(GetIdCompte());
            List<TypeDePrestation> typesDuPresta = prestataire.Prestations.Select(Prestation => Prestation.Type).ToList();

            //On doit lui passer Evenements ayant dappel de prestation concernants
            AppelDoffreViewModel viewModel = new AppelDoffreViewModel
            {
                EvenementInteresse = new EvenementService().ObtenirEvenement(idEvenement),
                PrestationsDuPrestataire = prestataire.Prestations.Where(p =>  p.Type == type).ToList(),
            };

            return viewModel;
        }
    }
}
