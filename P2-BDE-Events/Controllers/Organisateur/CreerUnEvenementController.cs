using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace P2_BDE_Events.Controllers.Organisateur
{
    public class CreerUnEvenementController : Controller
    {
        private EvenementService evenementService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreerUnEvenementController(IWebHostEnvironment webHostEnvironment)
        {
            evenementService = new EvenementService();
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure1()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement { Titre = "HELLOOOO"},
            };

            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure1(EvenementViewModel model)
        {
            if (ModelState.IsValid) {

                HttpContext.Session.SetString("Event", JsonConvert.SerializeObject(model));

                return RedirectToAction("CreerEvenementSurMesure2");
            }
            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", model);
        }


        [HttpGet]
        public IActionResult CreerEvenementSurMesure2()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement(),
            };

            return View("~/Views/Organisateur/CreerEvenementSurMesure2.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure2(EvenementViewModel model)
        {
            if (ModelState.IsValid)
            {
                string serializedEnementViewModel = HttpContext.Session.GetString("Event");
                EvenementViewModel vieuxEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

                //Manque ajoutes les proprietes par rapport à la region, etc
                vieuxEvent.Evenement.DateEvenement = model.Evenement.DateEvenement;

                HttpContext.Session.SetString("Event", JsonConvert.SerializeObject(vieuxEvent));
                return RedirectToAction("CreerEvenementSurMesure3");
            }
            return View("~/Views/Organisateur/CreerEvenementSurMesure2.cshtml", model);
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure3()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement(),
            };
            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure3(EvenementViewModel model)
        {            
            if (ModelState.IsValid)
            {
                string serializedEnementViewModel = HttpContext.Session.GetString("Event");
                EvenementViewModel vieuxEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

                vieuxEvent.Evenement.NbParticipants = model.Evenement.NbParticipants;
                vieuxEvent.Alcool = model.Alcool;
                vieuxEvent.Restauration = model.Restauration;
                vieuxEvent.Securite = model.Securite;
                vieuxEvent.Bar = model.Bar;

                HttpContext.Session.SetString("Event", JsonConvert.SerializeObject(vieuxEvent));
                return RedirectToAction("CreerEvenementSurMesure4");
            }
            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", model);
        }

        public IActionResult CreerEvenementSurMesure4()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement(),
            };

            return View("Views/Organisateur/CreerEvenementSurMesure4.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure4(EvenementViewModel nouveauEvent)
        {
            string serializedEnementViewModel = HttpContext.Session.GetString("Event");
            EvenementViewModel vieuxEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

            vieuxEvent.CoverPhoto = nouveauEvent.CoverPhoto;


            //si tout les trucs requis son good
            //creation evenement
            int idNouveauEvent = evenementService.CreerEvenement(vieuxEvent.Evenement);

            string str = _webHostEnvironment.WebRootPath;
            //Sauvegarder image dans nos dossiers
            if (nouveauEvent.CoverPhoto!= null & nouveauEvent.CoverPhoto.Length > 0)
            {
                string imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(nouveauEvent.CoverPhoto.FileName)}";
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "evenement", idNouveauEvent.ToString());

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string imagePath = Path.Combine(folderPath, imageFileName);


                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    nouveauEvent.CoverPhoto.CopyTo(stream);
                }

                Evenement evenementCree = evenementService.ObtenirEvenement(idNouveauEvent);
                evenementCree.CoverPhotoPath = imagePath;
                evenementService.ModifierEvenement(idNouveauEvent, evenementCree);

            }

            return View("View/Organisateur/MesEvenements/EvenementsEnCours");
        }

        public EvenementViewModel GetEventSession()
        {
            return null;
        }

        public void SetEventSession(EvenementViewModel evenement)
        {

        }
    }
}
