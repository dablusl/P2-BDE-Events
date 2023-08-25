using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Prestations;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers
{
    public class PrestationController : Controller
    {
        public IActionResult ToutesLesPrestations()
        {
            using (PrestationService prestationService = new PrestationService())
            {
                ListePrestationsViewModel viewModel = new ListePrestationsViewModel
                {
                    Prestations = prestationService.ObtenirToutesLesPrestations()
                };
                return View(viewModel);
            }           
        }
    }    
}

