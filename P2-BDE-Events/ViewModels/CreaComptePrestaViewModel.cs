using Microsoft.AspNetCore.Mvc.Rendering;
using P2_BDE_Events.Models.Compte;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class CreaComptePrestaViewModel
    {
        public Compte Compte { get; set; } 
        public Prestataire Prestataire { get; set; }
        public List<string> SelectedServiceTypes { get; set; }
        public List<SelectListItem> AvailableServiceTypes { get; set; }
    }
}
