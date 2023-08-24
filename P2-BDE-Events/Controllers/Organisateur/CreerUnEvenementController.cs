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

namespace P2_BDE_Events.Controllers.Organisateur
{
    public class CreerUnEvenementController : Controller
    {
        private EvenementService evenementService;

        public CreerUnEvenementController(IHttpContextAccessor httpContextAccessor)
        {
            evenementService = new EvenementService();
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure1()
        {

            var model = new Etape1ViewModel();
            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", model);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure1(Etape1ViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Etape1Data", JsonConvert.SerializeObject(model));
                return RedirectToAction("CreerEvenementSurMesure2");
            }

            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", model);
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure2()
        {
            var model = new Etape2ViewModel();
            return View("~/Views/Organisateur/CreerEvenementSurMesure2.cshtml", model);
        }

            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement(),
                TypeEvenements = typeEvenements
            };

            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);
            HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

        [HttpGet]
        public IActionResult CreerEvenementSurMesure3()
        {
            var model = new Etape3ViewModel();
            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", model);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure(EvenementViewModel nouveauEvent)
        {
            string serializedEnementViewModel = HttpContext.Session.GetString("EventViewModel");
            EvenementViewModel vieuxEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);
            
            vieuxEvent.Evenement.Titre = nouveauEvent.Evenement.Titre;
            vieuxEvent.Evenement.Type = nouveauEvent.Evenement.Type;

            serializedEnementViewModel = JsonConvert.SerializeObject(vieuxEvent);

            HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            HttpContext.Session.Remove("Etape1Data");
            HttpContext.Session.Remove("Etape2Data");
            HttpContext.Session.Remove("Etape3Data");

        public IActionResult CreerEvenementSurMesure2()
        {

            string serializedEnementViewModel = HttpContext.Session.GetString("EventViewModel");  
            EvenementViewModel nouveauEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

            return View("Views/Organisateur/CreerEvenementSurMesure2.cshtml", nouveauEvent);
        }
        [HttpPost]
        public IActionResult CreerEvenementSurMesure2(EvenementViewModel nouveauEvent)
        {
            string serializedEnementViewModel = HttpContext.Session.GetString("EventViewModel");
            EvenementViewModel vieuxEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

            vieuxEvent.Evenement.DateEvenement = nouveauEvent.Evenement.DateEvenement;

            serializedEnementViewModel = JsonConvert.SerializeObject(vieuxEvent);

            HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return RedirectToAction("CreerEvenementSurMesure3");
        }

        public IActionResult CreerEvenementSurMesure3()
        {
            string serializedEnementViewModel = HttpContext.Session.GetString("EventViewModel");

            EvenementViewModel nouveauEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);
            return View("Views/Organisateur/CreerEvenementSurMesure3.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure3(EvenementViewModel nouveauEvent)
        {
            string serializedEnementViewModel = HttpContext.Session.GetString("EventViewModel");
            EvenementViewModel vieuxEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

            vieuxEvent.Evenement.NbParticipants = nouveauEvent.Evenement.NbParticipants;

            serializedEnementViewModel = JsonConvert.SerializeObject(vieuxEvent);

            HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return RedirectToAction("CreerEvenementSurMesure4");
        }

        public IActionResult CreerEvenementSurMesure4()
        {
            //3 trucs optionnels photo etc ?
            //fournir photo evenement, cocher options
            //TODO : options
            string serializedEnementViewModel = _httpContextAccessor.HttpContext.Session.GetString("EventViewModel");

            EvenementViewModel nouveauEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

            return View("Views/Organisateur/CreerEvenementSurMesure4.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure4(EvenementViewModel nouveauEvent)
        {
            string serializedEnementViewModel2 = JsonConvert.SerializeObject(nouveauEvent);

            //si tout les trucs requis son good
            //creation evenement
            int idNouveauEvent = evenementService.CreerEvenement(nouveauEvent.Evenement);

            //Sauvegarder image dans nos dossiers
            if (nouveauEvent.CoverPhoto!= null & nouveauEvent.CoverPhoto.Length > 0)
            {
                string imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(nouveauEvent.CoverPhoto.FileName)}";
                //string imagePath = Path.Combine("/images/evenement/", imageFileName);
                string imagePath = Path.Combine("wwwroot", "images", "evenement", idNouveauEvent.ToString());

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    nouveauEvent.CoverPhoto.CopyTo(stream);
                }
                //associer path dimage à levenement
                evenementService.ObtenirEvenement(idNouveauEvent).CoverPhotoPath = imagePath;
            }

            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);

            HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return View("View/Organisateur/MesEvenements/EvenementsEnCours");
        }
    }
}
