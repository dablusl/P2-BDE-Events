using System;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class CommentaireEvenement : Commentaire
    {
        public int Id { get; set; }
        public int EvenmentId { get; set; }
        public virtual Evenement Evenement{ get; set; }
    }
}
