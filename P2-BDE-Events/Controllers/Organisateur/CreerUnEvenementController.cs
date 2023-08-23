using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private IHttpContextAccessor _httpContextAccessor;
        private EvenementService evenementService;

        public CreerUnEvenementController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            evenementService = new EvenementService();
        }

        public IActionResult ChoixCreation()
        {
            return View("Views/Organisateur/ChoixCreation.cshtml");
        }

        public IActionResult CreerEvenementSurMesure()
        {
            List<SelectListItem> typeEvenements = new List<SelectListItem>();

            foreach (TypeEvenement type in Enum.GetValues(typeof(TypeEvenement)))
            {
                //1 Etape -init evenement
                //Saisir Un Titre et choisir le type d'evenement
                //Init EvenementViewModel
                //TODO : ajouter contraintes de input de texte

                SelectListItem selectList = new SelectListItem() { 
                    Text = type.ToString(), 
                    Value = type.ToString() 
                };
                typeEvenements.Add(selectList);
            }

            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement(),
                TypeEvenements = typeEvenements
            };

            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);


            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return View("Views/Organisateur/CreerEvenementSurMesure.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure(EvenementViewModel nouveauEvent)
        {
            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);
            string checking = nouveauEvent.Evenement.Titre;

            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return RedirectToAction("CreerEvenementSurMesure2");
        }

        public IActionResult CreerEvenementSurMesure2()
        {
            //2 Etape -Ou et quand ?
            //donner zone de reference date et heure souhaitée
            //TODO : comment on va gerer les zone ? departement ? region ? CP? ...etc?

            string serializedEnementViewModel = _httpContextAccessor.HttpContext.Session.GetString("EventViewModel");
            
            EvenementViewModel nouveauEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);

            string checking = nouveauEvent.Evenement.Titre;
            return View("Views/Organisateur/CreerEvenementSurMesure2.cshtml", nouveauEvent);
        }
        [HttpPost]
        public IActionResult CreerEvenementSurMesure2(EvenementViewModel nouveauEvent)
        {
            string checking = nouveauEvent.Evenement.Titre;
            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);

            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return RedirectToAction("CreerEvenementSurMesure3");
        }

        public IActionResult CreerEvenementSurMesure3()
        {
            //3 Etape -Quoi et combien ?
            //Saisir nb de participants et choisir les services attendus
            //TODO : affichage de prestations à choisir 
            string serializedEnementViewModel = _httpContextAccessor.HttpContext.Session.GetString("EventViewModel");

            EvenementViewModel nouveauEvent = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);
            return View("Views/Organisateur/CreerEvenementSurMesure3.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure3(EvenementViewModel nouveauEvent)
        {
            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);

            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

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

            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return View("View/Organisateur/MesEvenements/EvenementsEnCours");
        }
    }
}

