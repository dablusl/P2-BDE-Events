using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class MesEvenementsPrestaController : Controller
    {
        private EvenementService evenementService;
        private PrestataireService prestataireService;

        public MesEvenementsPrestaController()
        {
            evenementService = new EvenementService();
            prestataireService = new PrestataireService();
        }

        public IActionResult Index()
        {
            EvenementPrestaViewModel evenements = new EvenementPrestaViewModel
            {
                Evenements = evenementService.EvenementsPrestataire(GetIdCompte(), Models.Evenement.Enums.EtatEvenement.PUBLIE),
                IdPrestataire = prestataireService.GetPrestataireParCompte(GetIdCompte()).Id,
            };

            return View("~/Views/Prestation/EvenementsAVenir.cshtml", evenements);
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }
    }
}
