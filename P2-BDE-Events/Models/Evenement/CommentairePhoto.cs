using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class CommentairePhoto : Commentaire
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
