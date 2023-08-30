using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class AppelsDoffreController : Controller
    {
        //afficher toutes les appels d'offre possibles par prestations du prestataire
        public IActionResult Index()
        {
            return View();
        }
    }
}
