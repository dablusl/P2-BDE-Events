using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P2_BDE_Events.Models.Evenement;
using System;

namespace P2_BDE_Events.Controllers
{
    public class OrganisateurController : Controller
    {
        public IActionResult MesEvenements()
        {
            return View();
        }

        public IActionResult CreerEvenement()
        {
            Evenement nouveauEvenement = new Evenement { Titre = "Ici"};
            return View(nouveauEvenement);
        }

        [HttpPost]
        public IActionResult CreerEvenement(Evenement nouveauEvenement)
        {
            string evenementJson = JsonConvert.SerializeObject(nouveauEvenement);
            HttpContext.Session.SetString("EvenementData", evenementJson);
            return RedirectToAction("CreerEvenement2");
        }
        

        public IActionResult CreerEvenement2()
        {
            string evenementJson = HttpContext.Session.GetString("EvenementData");

            if (!string.IsNullOrEmpty(evenementJson))
            {
                Evenement evenement = JsonConvert.DeserializeObject<Evenement>(evenementJson);
                return View(evenement);
            }
            else
            {
                // Handle the case where the session data is not available
                return RedirectToAction("CreerEvenement");
            }
        }


    }
}

