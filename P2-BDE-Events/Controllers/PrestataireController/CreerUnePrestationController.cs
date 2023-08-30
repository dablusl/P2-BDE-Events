using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.DataAccessLayer; 
using P2_BDE_Events.Models.Prestations;

namespace P2_BDE_Events.Controllers
{
    public class PrestationController : Controller
    {
        private readonly BDDContext _dbContext; 

        public PrestationController(BDDContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [HttpGet]
        public IActionResult Creer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Creer(Prestation prestation)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Prestations.Add(prestation);
                _dbContext.SaveChanges();
                return RedirectToAction("Index"); // Redirigez où vous le souhaitez
            }

            return View(prestation);
        }

        [HttpGet]
        public IActionResult Modifier(int id)
        {
            var prestation = _dbContext.Prestations.Find(id);
            if (prestation == null)
            {
                return NotFound();
            }

            return View(prestation);
        }

        [HttpPost]
        public IActionResult Modifier(Prestation prestation)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Prestations.Update(prestation);
                _dbContext.SaveChanges();
                return RedirectToAction("Index"); // Redirigez où vous le souhaitez
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

        [HttpPost, ActionName("Supprime")]
        public IActionResult SuppressionConfirmee(int id)
        {
            var prestation = _dbContext.Prestations.Find(id);
            if (prestation == null)
            {
                return NotFound();
            }

            _dbContext.Prestations.Remove(prestation);
            _dbContext.SaveChanges();

            return RedirectToAction("Index"); // Redirigez où vous le souhaitez
        }
    }
}
