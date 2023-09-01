using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Prestations
{
    public class Prestation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Titre { get; set; }
        
        [Required]
        public int CapaciteMax { get; set; }
        [Required]
        public int Tarif { get; set; }
        public DateTime Calendrier { get; set; }
        [Required]
        public bool Livraison { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = null!;


        public EtatDePrestation Etat { get; set; }
        [Required]
        public TypeDePrestation Type { get; set; }
       // public TypeDeLocation Location { get; set; }
        
        public int PrestataireId { get; set; }
        public virtual Prestataire Prestataire { get; set; }
    }
}