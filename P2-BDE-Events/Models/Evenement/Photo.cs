using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement.Enums;

namespace P2_BDE_Events.Models.Evenement
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public EtatPhoto Etat {get; set; }
        public virtual Album Album { get; set; }
        public virtual Compte Author { get; set; }
    }
}