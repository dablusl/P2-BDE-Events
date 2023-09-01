using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Prestations;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Controllers.PrestataireController
{


    public class ConsultationPrestationsController : Controller
    {
        private PrestationService prestationservice;
        private PrestataireService prestataireservice;

        public ConsultationPrestationsController()
        {
            prestationservice = new PrestationService();
            prestataireservice = new PrestataireService();
        }
        public IActionResult ToutesLesPrestations()
        {
            Prestataire prestataire = prestataireservice.GetPrestataireParCompte(GetIdCompte());

            List<Prestation> prestationsDuPrestataire = prestationservice
                   .ObtenirPrestationsParPrestataire(prestataire.Id);

            return View("~/Views/Prestation/ToutesLesPrestations.cshtml", prestationsDuPrestataire);

        }
        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }
    

    }
}

