using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                prestataire = prestataireService.GetPrestataireParCompte(int.Parse(HttpContext.Session.GetString("iDCompte"))),
                prestation = new Prestation()
            };


            return View("~/Views/Prestation/CreerUnePrestation.cshtml", viewModelLocal); ;
        }

        [HttpPost]
        public IActionResult CreerUnePrestation(UnePrestationViewsModel viewModel)
        {
            PrestataireService prestataireService = new PrestataireService();
            viewModel.prestataire = prestataireService.GetPrestataireParCompte(int.Parse(HttpContext.Session.GetString("iDCompte")));
            viewModel.prestation.PrestataireId = viewModel.prestataire.Id;

            if (ModelState.IsValid)
            {

                _dbContext.Prestations.Add(viewModel.prestation);
                _dbContext.SaveChanges();
                return RedirectToAction("ToutesLesPrestations", "ConsultationPrestations", new { area = "PrestataireControllers" });
            }

            return View("~/Views/Prestation/CreerUnePrestation.cshtml", viewModel);
        }

        [HttpGet]

        public IActionResult ModifierUnePrestation(int id)
        {
            PrestataireService prestataireService = new PrestataireService();

            var viewModel = new UnePrestationViewsModel()
            {
                prestataire = prestataireService.GetPrestataireParCompte(int.Parse(HttpContext.Session.GetString("iDCompte"))),
                prestation = _dbContext.Prestations.Find(id)
            };
            if (viewModel == null)
            {
                return NotFound();
            }

            return View("~/Views/Prestation/ModifierUnePrestation.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult ModifierUnePrestation(UnePrestationViewsModel viewModel)
        {
            if (ModelState.IsValid)
            {

                PrestationService prestationService = new PrestationService();
                prestationService.ModifierPrestation(viewModel.prestation.Id, viewModel.prestation);
                return RedirectToAction("ToutesLesPrestations", "ConsultationPrestations", new { area = "PrestataireControllers" });

            }

            return View("~/Views/Prestation/ToutesLesPrestations.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult SupprimerUnePrestation(int id)
        {
            var prestation = _dbContext.Prestations.Find(id);
            if (prestation == null)
            {
                return NotFound();
            }

            _dbContext.Prestations.Remove(prestation);
            _dbContext.SaveChanges();

            return RedirectToAction("ToutesLesPrestations", "ConsultationPrestations");
        }

    }
}
