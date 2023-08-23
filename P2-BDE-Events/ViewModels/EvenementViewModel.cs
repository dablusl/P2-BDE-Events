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

        public TypeEvenement SelectedType { get; set; }

        public List<TypeEvenement> TypeEvenements { get; set;}
    }
}
