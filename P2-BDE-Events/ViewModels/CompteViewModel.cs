using Microsoft.AspNetCore.Mvc.Rendering;
using P2_BDE_Events.Models.Comptes;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class CompteViewModel
    {
        public Compte Compte { get; set; }
        public bool Authentifie { get; set; }


    }
}
