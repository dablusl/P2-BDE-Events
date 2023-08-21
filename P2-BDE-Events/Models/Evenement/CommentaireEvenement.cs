using System;

namespace P2_BDE_Events.Models.Evenement
{
    public class CommentaireEvenement : Commentaire
    {
        public int Id { get; set; }
        public string Contenu { get; set; }
        public int Likes { get; set; }

        public DateTime PublieLe { get; set; }
        public int IdEvenement;
        public int IdParticipant;
    }
}
