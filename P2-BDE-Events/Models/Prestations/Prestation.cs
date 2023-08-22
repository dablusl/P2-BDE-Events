﻿using System.Collections.Generic;

namespace P2_BDE_Events.Models.Prestations
{
    public class Prestation
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int CapaciteMax { get; set; }
        public decimal Tarif { get; set; }
        public string Calendrier { get; set; }
        public bool Livraison { get; set; }
        public string Description { get; set; }
        public Enums.EtatDePrestation Etat { get; set; }
        public Enums.TypeDePrestation Type { get; set; }
        
    }
}