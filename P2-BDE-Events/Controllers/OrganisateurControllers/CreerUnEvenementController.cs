using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

                savedEvent.Evenement.NbParticipants = model.Evenement.NbParticipants;

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

            return View("~/Views/Organisateur/MesEvenements/EvenementsEnCours");
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
            HttpContext
                .Session
                .SetString(
                    "Event",
                    JsonConvert.SerializeObject(evenementViewModel)
                    );
        }

        public int CreationEvenementBD(EvenementViewModel nouveauEvent)
        {
            string idCompte = HttpContext.Session.GetString("iDCompte");
            Compte compte = new CompteService().ObtenirCompte(idCompte);
            Participant participant = new ParticipantService().ObtenirParticipant(compte);
            Organisateur organisateur = new OrganisateurService().ObtenirOrganisateur(participant);


            int idNouveauEvent = evenementService.CreerEvenement(GetEventSession().Evenement);
            Evenement evenementCree = evenementService.ObtenirEvenement(idNouveauEvent);
            evenementCree.CoverPhotoPath = SaveCoverPhoto(nouveauEvent, idNouveauEvent);
            evenementCree.Organisateur = organisateur;
            evenementService.ModifierEvenement(idNouveauEvent, evenementCree);

            return idNouveauEvent;
        }

        public void CreationLignesEvenement(int idEvenement)
        {   
            foreach( var type in GetEventSession().Types )
            {
                if (type.Value)
                {
                    LigneEvenement nouvelleLigne = new LigneEvenement
                    { 
                        Type = type.Key
                    };

                    int idligne = ligneEvenementService.CreerLigneEvenement(nouvelleLigne);
                    LigneEvenement novuelleLigne = ligneEvenementService.ObtenirLigneEvenement(idligne);
                    ligneEvenementService.ModifierLigneEvenement(idligne, evenementService.ObtenirEvenement(idEvenement));
                }

            }
        }

        public string SaveCoverPhoto(EvenementViewModel nouveauEvent, int idNouveauEvent)
        {
            if (nouveauEvent.CoverPhoto != null & nouveauEvent.CoverPhoto.Length > 0)
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

                return imagePath;
            }

            return "";
        }

        public Organisateur GetOrganisateurEvenement()
        {
            string idCompte = HttpContext.Session.GetString("iDCompte");
            Compte compte = new CompteService().ObtenirCompte(idCompte);
            Participant participant = new ParticipantService().ObtenirParticipant(compte);
            Organisateur organisateur = new OrganisateurService().ObtenirOrganisateur(participant);

            return organisateur;
        }
    }
}