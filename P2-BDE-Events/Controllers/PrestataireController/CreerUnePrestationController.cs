using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Services.Evenements;
using System.Collections.Generic;

namespace P2_BDE_Events.Controllers.PrestataireController
{
    public class CreerUnePrestationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
