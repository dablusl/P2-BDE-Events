using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Prestations;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class ConsultationPrestationsController : Controller
    {
        public IActionResult ToutesLesPrestations()
        {
            using (PrestationService prestationService = new PrestationService())
            {
                ListePrestationsViewModel viewModel = new ListePrestationsViewModel
                {
                    Prestations = prestationService.ObtenirToutesLesPrestations()
                };
                return View("~/Views/Prestation/ToutesLesPrestations.cshtml", viewModel);
            }
        }
    }
}

