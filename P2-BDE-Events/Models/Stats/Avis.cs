using P2_BDE_Events.Models.Comptes;
using System;

namespace P2_BDE_Events.Models.Stats
{
    public class Avis
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Contenu { get; set; }
        public DateTime PublieLe { get; set; }
        public double Notation { get; set; }
        public virtual Compte Compte { get; set; }
    }
}
