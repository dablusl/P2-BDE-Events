using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.DataAccessLayer; 
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Prestations;
using P2_BDE_Events.ViewModels;
using System;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class CreerUnePrestationController : Controller
    {
        private readonly BDDContext _dbContext; 

        public CreerUnePrestationController()
        {
            _dbContext = new BDDContext();
        }

        
        [HttpGet]
        public IActionResult CreerUnePrestation()
        {
            PrestataireService prestataireService = new PrestataireService();

            
            var viewModelLocal = new UnePrestationViewsModel()
            {
                prestataire = prestataireService.GetPrestataireParCompte(int.Parse(HttpContext.Session.GetString("iDCompte")))
            };
            
            
            return View("~/Views/Prestation/CreerUnePrestation.cshtml",viewModelLocal); ;
        }

        [HttpPost]
        public IActionResult CreerUnePrestation(UnePrestationViewsModel viewModel)
        {
            if (ModelState.IsValid)
            {
                PrestataireService prestataireService = new PrestataireService();
                viewModel.prestataire = prestataireService.GetPrestataireParCompte(viewModel.prestation.PrestataireId); 
                _dbContext.Prestations.Add(viewModel.prestation);
                _dbContext.SaveChanges();
                return View("~/");
            }
            PrestataireService prestataireServicefail = new PrestataireService();
            int.TryParse(HttpContext.Session.GetString("Sid"), out int myId);
            Console.WriteLine("-----------------------------------" + myId);

            viewModel.prestataire = prestataireServicefail.GetPrestataireParCompte(int.Parse(HttpContext.Session.GetString("iDCompte")));
            return View("~/Views/Prestation/CreerUnePrestation.cshtml",viewModel);
        }

        [HttpGet]
        public IActionResult Modifier(int id)
        {
            var prestation = _dbContext.Prestations.Find(id);
            if (prestation == null)
            {
                return NotFound();
            }

            return View("~/Views/Prestation/ModifierUnePrestation.cshtml",prestation);
        }

        [HttpPost]
        public IActionResult Modifier(Prestation prestation)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Prestations.Update(prestation);
                _dbContext.SaveChanges();
                return View("~/Views/Prestation/ModifierUnePrestation.cshtml");
            }

            return View(prestation);
        }

        [HttpGet]
        public IActionResult Supprimer(int id)
        {
            var prestation = _dbContext.Prestations.Find(id);
            if (prestation == null)
            {
                return NotFound();
            }

            return View("~/Views/Prestation/ModifierUnePrestation.cshtml"); ;
        }

        [HttpPost, ActionName("Supprimer")]
        public IActionResult SuppressionConfirmee(int id)
        {
            var prestation = _dbContext.Prestations.Find(id);
            if (prestation == null)
            {
                return NotFound();
            }

            _dbContext.Prestations.Remove(prestation);
            _dbContext.SaveChanges();

            return View("~/Views/Prestation/SupprimerUnePrestation.cshtml"); 
        }

    }
}
