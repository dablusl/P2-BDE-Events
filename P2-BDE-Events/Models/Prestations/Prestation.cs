﻿using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Prestations
{
    public class Prestation
    { 
        public int Id { get; set; }  
        public string Titre { get; set; }  
        public int CapaciteMax { get; set; }      
        public int Tarif { get; set; }
        public DateTime Calendrier { get; set; }
        public bool Livraison { get; set; }
        public string Description { get; set; } = null!;
        public EtatDePrestation Etat { get; set; }
        public TypeDePrestation Type { get; set; }
       // public TypeDeLocation Location { get; set; }
        
        public int PrestataireId { get; set; }
        public virtual Prestataire Prestataire { get; set; }
    }
}