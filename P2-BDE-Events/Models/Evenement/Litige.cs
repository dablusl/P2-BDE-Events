using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement.Enums;
using System;

namespace P2_BDE_Events.Models.Evenement
{
    public class Litige
    {
        public int Id { get; set; }
        public EtatLitige Etat { get; set; }
        public TypeLitige Type { get; set; }
        public string Description { get; set; }
        public DateTime CreeLe {  get ; set; }
        public virtual Compte Compte { get; set; }

        public Litige(Compte compte)
        {
            Compte = compte;
        }
    }
}
