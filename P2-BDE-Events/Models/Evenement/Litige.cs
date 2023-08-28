using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class Litige
    {
        public int Id { get; set; }
        public EtatLitige Etat { get; set; }
        public TypeLitige Type { get; set; }
        public string Description { get; set; }
        public DateTime CreeLe {  get ; set; }
        [Required]
        public virtual Compte Compte { get; set; }

    }
}
