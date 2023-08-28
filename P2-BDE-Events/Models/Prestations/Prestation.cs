using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations.Enums;
using System.Collections.Generic;

namespace P2_BDE_Events.Models.Prestations
{
    public class Prestation
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int CapaciteMax { get; set; }
        public double Tarif { get; set; }
        public string Calendrier { get; set; }
        public bool Livraison { get; set; }
        public string Description { get; set; }
        public EtatDePrestation Etat { get; set; }
        public TypeDePrestation Type { get; set; }
        public virtual Prestataire Prestataire { get; set; }
        
    }
}