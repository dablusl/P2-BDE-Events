using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Evenement.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace P2_BDE_Events.ViewModels
{
    public class EvenementViewModel
    {
        public Evenement Evenement { get; set; }

        //public List<SelectListItem> TypeEvenements { get; set;}

        public IFormFile CoverPhoto { get; set; }

        public DateTime DateTimeTest { get; set; }

        [Display(Name = "Alcool")]
        public bool Alcool { get; set; }

        [Display(Name = "Restauration")]
        public bool Restauration { get; set; }

        [Display(Name = "Sécurité")]
        public bool Securite { get; set; }

        [Display(Name = "Bar")]
        public bool Bar { get; set; }
    }
}
