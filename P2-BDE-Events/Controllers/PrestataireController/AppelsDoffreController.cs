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
        public IActionResult ConsulterLesAppels()
        {
            return View("~/Views/Prestation/ConsulterLesAppels.cshtml",InitViewModel());
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }
        public AppelDoffreViewModel InitViewModel()
        {

            Prestataire prestataire = new PrestataireService().GetPrestataireParCompte(GetIdCompte());
            List<TypeDePrestation> typesDuPresta = prestataire.Prestations.Select(Prestation => Prestation.Type).ToList();
            List<LigneEvenement> lignesEnAppelDOffre = new LigneEvenementService().AppelsDoffre(typesDuPresta);

            AppelDoffreViewModel viewModel = new AppelDoffreViewModel();

            foreach (LigneEvenement ligne in lignesEnAppelDOffre)
            {
                viewModel.Lignes.Add(new AppelDoffreViewModel.LigneAProposer
                    {
                        Ligne = ligne,
                        OnParticipe = false,
                    });
            }

            return viewModel;
        }
    }
}
