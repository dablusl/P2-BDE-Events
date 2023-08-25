using Microsoft.AspNetCore.Mvc.Rendering;
using P2_BDE_Events.Models.Compte;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class CreationCompteViewModel
    {
        public Compte Compte { get; set; }
        public string SelectedProfile { get; set; } 
        public List<SelectListItem> AvailableProfiles { get; set; }
    }
}
