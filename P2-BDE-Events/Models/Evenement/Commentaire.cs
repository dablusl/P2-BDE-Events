using P2_BDE_Events.Models.Comptes;
using System;

namespace P2_BDE_Events.Models.Evenement
{
    public abstract class Commentaire
    {
        public string Contenu { get; set; }
        public int Likes { get; set; }
        public DateTime PublieLe { get; set; }
        public int CompteId { get; set; }
        public Compte Compte { get; set; }
    }
}
