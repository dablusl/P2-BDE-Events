using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace P2_BDE_Events.ViewModels
{
    public class EvenementViewModel
    {
        public Evenement Evenement { get; set; }

        public IFormFile CoverPhoto { get; set; }
        public Dictionary<TypeDePrestation, bool> Types { get; set; }

        public EvenementViewModel()
        {
            Types = new Dictionary<TypeDePrestation, bool>();
            foreach (TypeDePrestation prestation in Enum.GetValues(typeof(TypeDePrestation)))
            {
                Types[prestation] = false;
            }
        }
    }
}
