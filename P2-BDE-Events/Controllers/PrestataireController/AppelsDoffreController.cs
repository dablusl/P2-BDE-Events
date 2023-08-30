using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class AppelsDoffreController : Controller
    {
        //afficher toutes les appels d'offre possibles par prestations du prestataire
        public IActionResult Index()
        {
            Prestataire prestataire = new PrestataireService().GetPrestataireParCompte(GetIdCompte());
            //List<TypeDePrestation> typesDePrestationDuPrestataire = prestataire.Prestations.Map

            List<Prestation> appelDOffre = new LigneEvenementService().AppelsDoffre(prestataire.Prestations);

            return View();
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }
    }
}
