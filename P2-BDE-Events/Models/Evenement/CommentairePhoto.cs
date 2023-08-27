namespace P2_BDE_Events.Models.Evenement
{
    public class CommentairePhoto : Commentaire
    {
        public int Id { get; set; }
        public virtual Photo Photo { get; set; }

        public CommentairePhoto(Photo photo)
        {
            Photo = photo;
        }
    }
}
