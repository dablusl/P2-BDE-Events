using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.ViewModels;
using System;

namespace P2_BDE_Events.Controllers
{
    public class OrganisateurController : Controller
    {
        //DataTemp
        //ViewData ...
        //Session
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrganisateurController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult MesEvenements()
        {
            return View();
        }

        public IActionResult Etape1()
        {
            EvenementViewModel nouveauEvent = new EvenementViewModel
            {
                Evenement = new Evenement()
            };

            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);

            /*
             * obj = {
             *              prop1 : kfhoas
             *              prop2 : qslkfhq
             *       }
             *       
             *  obj="{prop1:kfds,prop2:shgqd}"     
             */

            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return View(nouveauEvent);
        }

        [HttpPost]
        public IActionResult Etape1(EvenementViewModel nouveauEvent)
        {

            string serializedEnementViewModel = JsonConvert.SerializeObject(nouveauEvent);
            
            _httpContextAccessor.HttpContext.Session.SetString("EventViewModel", serializedEnementViewModel);

            return RedirectToAction("Etape2");
        }

        public IActionResult Etape2()
        {
            string serializedEnementViewModel = _httpContextAccessor.HttpContext.Session.GetString("EventViewModel");

            EvenementViewModel evenementViewModel = JsonConvert.DeserializeObject<EvenementViewModel>(serializedEnementViewModel); 
            return View(evenementViewModel);
        }

    }
}

