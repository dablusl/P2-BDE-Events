using System;

namespace P2_BDE_Events.Models.Evenement
{
    public class CommentaireEvenement : Commentaire
    {
        public int Id { get; set; }
        public virtual Evenement Evenement{ get; set; }

        public CommentaireEvenement() { }

        public CommentaireEvenement(Evenement evenement)
        {
            Evenement = evenement;
        }
    }
}
