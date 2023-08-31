using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.DataAccessLayer; 
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Services.Prestations;
using P2_BDE_Events.ViewModels;

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
        public IActionResult Creer()
        {
            var viewModel = new UnePrestationViewsModel();
            return View("~/Views/Prestation/CreerUnePrestation.cshtml",viewModel); ;
        }

        [HttpPost]
        public IActionResult Creer(UnePrestationViewsModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Prestations.Add(viewModel.prestation);
                _dbContext.SaveChanges();
                return View("~/Views/Prestation/CreerUnePrestation.cshtml");
            }

            return View(viewModel);
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

            return View(prestation);
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
