using P2_BDE_Events.Models.Evenement.Enums;

namespace P2_BDE_Events.Models.Evenement
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public int IdAuthor { get; set; }
        public EtatPhoto Etat {get; set; }
        public int IdAlbum { get; set; }
    }
}