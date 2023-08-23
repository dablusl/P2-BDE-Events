using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Evenement.Enums;
using System;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class EvenementViewModel
    {
        public Evenement Evenement { get; set; }

        public List<SelectListItem> TypeEvenements { get; set;}

        public IFormFile CoverPhoto { get; set; }

        public DateTime DateTimeTest { get; set; }
    }
}
