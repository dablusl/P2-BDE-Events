using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Evenement;
using System;

namespace P2_BDE_Events.Controllers
{
    public class ProfilOrganisateurController : Controller
    {
        public IActionResult MesEvenements()
        {
            return View();
        }

        public IActionResult CreerEvenement()
        {
            Evenement evenement = new Evenement { Titre="LOOOOL" };
            return View(evenement);
        }
        [HttpPost]
        public IActionResult CreerEvenement(Evenement evenement)
        {
            Console.WriteLine("hellor");
            return RedirectToAction("CreerEvenement2",evenement);
        }

        public IActionResult CreerEvenement2(Evenement evenement)
        {
            return View(evenement);
        }
    }
}
