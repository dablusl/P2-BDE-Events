﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P2_BDE_Events.ViewModels;
using System;

namespace P2_BDE_Events.Controllers.Organisateur
{
    public class CreerUnEvenementController : Controller
    {
        public IActionResult ChoixCreation()
        {
            return View("~/Views/Organisateur/ChoixCreation.cshtml");
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

        [HttpPost]
        public IActionResult CreerEvenementSurMesure2(Etape2ViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Etape2Data", JsonConvert.SerializeObject(model));
                return RedirectToAction("CreerEvenementSurMesure3");
            }

            return View("~/Views/Organisateur/CreerEvenementSurMesure2.cshtml", model);
        }

        [HttpGet]
        public IActionResult CreerEvenementSurMesure3()
        {
            var model = new Etape3ViewModel();
            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", model);
        }

        [HttpPost]
        public IActionResult CreerEvenementSurMesure3(Etape3ViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Etape3Data", JsonConvert.SerializeObject(model));
                return RedirectToAction("PageDeConfirmation");
            }

            return View("~/Views/Organisateur/CreerEvenementSurMesure3.cshtml", model);
        }

        [HttpGet]
        public IActionResult PageDeConfirmation()
        {
            var etape1Data = HttpContext.Session.GetString("Etape1Data");
            var etape2Data = HttpContext.Session.GetString("Etape2Data");
            var etape3Data = HttpContext.Session.GetString("Etape3Data");

            HttpContext.Session.Remove("Etape1Data");
            HttpContext.Session.Remove("Etape2Data");
            HttpContext.Session.Remove("Etape3Data");

            var viewModel = new PageDeConfirmationViewModel
            {
                Etape1Data = JsonConvert.DeserializeObject<Etape1ViewModel>(etape1Data),
                Etape2Data = JsonConvert.DeserializeObject<Etape2ViewModel>(etape2Data),
                Etape3Data = JsonConvert.DeserializeObject<Etape3ViewModel>(etape3Data)
            };

            return View("~/Views/Organisateur/PageDeConfirmation.cshtml", viewModel);
        }
    }
}
