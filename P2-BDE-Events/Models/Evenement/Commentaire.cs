using System;

namespace P2_BDE_Events.Models.Evenement
{
    public abstract class Commentaire
    {
        public string Contenu { get; set; }
        public int Likes { get; set; }
        public DateTime PublieLe { get; set; }
        public int IdAuthor { get; set; }
       
    }
}
