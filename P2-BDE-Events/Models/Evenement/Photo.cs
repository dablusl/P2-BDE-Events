using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement.Enums;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public EtatPhoto Etat {get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public int CompteId { get; set; }
        public virtual Compte Compte { get; set; }
    }
}