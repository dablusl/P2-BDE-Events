using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class CreerUnEvenementController : Controller
    {
        private EvenementService evenementService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private Organisateur organisateur;

        public CreerUnEvenementController(IWebHostEnvironment webHostEnvironment)
        {
            this.evenementService = new EvenementService();
            this._webHostEnvironment = webHostEnvironment;

            Compte compte = new CompteService().ObtenirCompte(HttpContext.Session.GetString("iDCompte"));

            this.organisateur = new OrganisateurService().ObtenirOrganisateur(compte);
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure1()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement { Organisateur = organisateur},
            };

            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure1(EvenementViewModel model)
        {
            if (ModelState.IsValid) {
                SetEventSession(model);
                
                return RedirectToAction("CreerEvenementSurMesure2");
            }
            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", model);
        }


        [HttpGet]
        public IActionResult CreerEvenementSurMesure2()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement { Organisateur = organisateur },
            };

            return View("~/Views/Organisateur/CreerEvenementSurMesure2.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure2(EvenementViewModel model)
        {
            if (ModelState.IsValid)
            {
                EvenementViewModel savedEvent = GetEventSession();

                savedEvent.Evenement.DateEvenement = model.Evenement.DateEvenement;

                SetEventSession(savedEvent);
                return RedirectToAction("CreerEvenementSurMesure3");
            }
            return View("~/Views/Organisateur/CreerEvenementSurMesure2.cshtml", model);
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure3()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement { Organisateur = organisateur },
            };
            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure3(EvenementViewModel model)
        {            
            if (ModelState.IsValid)
            {
                EvenementViewModel savedEvent = GetEventSession();

                savedEvent.Evenement.NbParticipants = model.Evenement.NbParticipants;

                foreach ( var typePrestation in savedEvent.Types)
                {
                    savedEvent.Types[typePrestation.Key] = model.Types[typePrestation.Key];
                }

                SetEventSession(savedEvent);

                return RedirectToAction("CreerEvenementSurMesure4");
            }
            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", model);
        }

        public IActionResult CreerEvenementSurMesure4()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement { Organisateur = organisateur },
            };

            return View("Views/Organisateur/CreerEvenementSurMesure4.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure4(EvenementViewModel nouveauEvent)
        {
            EvenementViewModel savedEvent = GetEventSession(); 
            
            savedEvent.CoverPhoto = nouveauEvent.CoverPhoto;


            //si tout les trucs requis son good
            //creation evenement
            int idNouveauEvent = evenementService.CreerEvenement(savedEvent.Evenement);

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

            return Redirect("Views/Organisateur/MesEvenements/EvenementsEnCours");
        }

        public EvenementViewModel GetEventSession()
        {
            return JsonConvert
                .DeserializeObject<EvenementViewModel>(
                    HttpContext.Session.GetString("Event")
                    ); 
        }

        public void SetEventSession(EvenementViewModel evenementViewModel)
        {
            HttpContext
                .Session
                .SetString(
                    "Event", 
                    JsonConvert.SerializeObject(evenementViewModel)
                    );
        }
    }
}