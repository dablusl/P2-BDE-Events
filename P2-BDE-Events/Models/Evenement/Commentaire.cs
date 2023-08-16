using System;

namespace P2_BDE_Events.Models.Evenement
{
    public abstract class Commentaire
    {
      
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime CreeLe { get; set; }
        public int IdAuthor { get; set; }
       
    }
}
