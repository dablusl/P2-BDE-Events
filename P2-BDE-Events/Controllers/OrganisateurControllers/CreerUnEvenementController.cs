using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System;
using System.Data;
using System.IO;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{

    [Authorize(Roles = "Organisateur")]
    public class CreerUnEvenementController : Controller
    {
        private EvenementService evenementService;
        private LigneEvenementService ligneEvenementService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IActionResult ChoixCreation() { return View("~/Views/Organisateur/ChoixCreation.cshtml"); }

        public CreerUnEvenementController(IWebHostEnvironment webHostEnvironment)
        {
            this.evenementService = new EvenementService();
            this.ligneEvenementService = new LigneEvenementService();
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure1()
        {
            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", InitViewModel());
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure1(EvenementViewModel model)
        {
            if (ModelState.IsValid)
            {
                SetEventSession(model);

                return RedirectToAction("CreerEvenementSurMesure2");
            }
            return View("~/Views/Organisateur/CreerEvenementSurMesure1.cshtml", model);
        }


        [HttpGet]
        public IActionResult CreerEvenementSurMesure2()
        {
            return View("~/Views/Organisateur/CreerEvenementSurMesure2.cshtml", InitViewModel());
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
            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", InitViewModel());
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure3(EvenementViewModel model)
        {
            if (ModelState.IsValid)
            {
                EvenementViewModel savedEvent = GetEventSession();

                savedEvent.Evenement.MaxParticipants = model.Evenement.MaxParticipants;

                foreach (var typePrestation in model.Types)
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
            return View("Views/Organisateur/CreerEvenementSurMesure4.cshtml", InitViewModel());
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure4(EvenementViewModel nouveauEvent)
        {

            int idNouveauEvenement = CreationEvenementBD(nouveauEvent);
            CreationLignesEvenement(idNouveauEvenement);

            return RedirectToAction("MesEvenementsOrga", "MesEvenements");
        }

        public EvenementViewModel InitViewModel()
        {
            return new EvenementViewModel
            {
                Evenement = new Evenement { Organisateur = new Organisateur() },
            };
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
            HttpContext.Session.SetString("Event", JsonConvert.SerializeObject(evenementViewModel));
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }

        public int CreationEvenementBD(EvenementViewModel nouveauEvent)
        {
            Organisateur organisateur = new OrganisateurService().GetOrganisateurParCompte(GetIdCompte());

            return evenementService.CreerEvenement(
                GetEventSession().Evenement,
                organisateur,
                SaveCoverPhoto(nouveauEvent));
        }

        public void CreationLignesEvenement(int idEvenement)
        {
            foreach (var type in GetEventSession().Types)
            {
                if (type.Value)
                {
                    ligneEvenementService.CreerLigneEvenement(type.Key, idEvenement);
                }
            }
        }

        public string SaveCoverPhoto(EvenementViewModel nouveauEvent)
        {
            if (nouveauEvent.CoverPhoto != null && nouveauEvent.CoverPhoto.Length > 0)
            {
                string imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(nouveauEvent.CoverPhoto.FileName)}";
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "evenement");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string imagePath = Path.Combine(folderPath, imageFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    nouveauEvent.CoverPhoto.CopyTo(stream);
                }

                return "/images/evenement/" + imageFileName; ;
            }

            return "/images/evenement/default.jpg";
        }
    }
}