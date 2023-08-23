using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.ViewModels;
using System;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers.Organisateur
{
    public class CreerUnEvenementController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CreerUnEvenementController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
                SelectListItem selectList = new SelectListItem() { Text = type.ToString(), Value = ((int)type).ToString() };
                typeEvenements.Add(selectList);
            }

            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement(), TypeEvenements = typeEvenements
            };

            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);

            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return View("Views/Organisateur/CreerEvenementSurMesure.cshtml", nouveauEvent);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure(EvenementViewModel nouveauEvent)
        {

            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);

            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return RedirectToAction("CreerEvenementSurMesure2");
        }

        public IActionResult CreerEvenementSurMesure2()
        {
            string serializedEnementViewModel = _httpContextAccessor.HttpContext.Session.GetString("EventViewModel");

            EvenementViewModel evenementViewModel = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel);
            return View("Views/Organisateur/CreerEvenementSurMesure2.cshtml", evenementViewModel);
        }

    }
}

