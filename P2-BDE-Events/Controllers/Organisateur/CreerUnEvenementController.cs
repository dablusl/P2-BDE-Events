using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P2_BDE_Events.Models.Evenement;
using System;

namespace P2_BDE_Events.Controllers.Organisateur
{
    public class CreerUnEvenementController : Controller
    {
        public IActionResult ChoixCreation() 
        { 
            return View("Views/Organisateur/ChoixCreation.cshtml"); 
        }

        public IActionResult CreerEvenementSurMesure()
        {
            return View();
        }

    }
}

