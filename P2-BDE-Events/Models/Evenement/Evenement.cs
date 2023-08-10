using System;

namespace P2_BDE_Events.Models.Evenement
{
    public class Evenement
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime DateEvenement { get; set; }
        public DateTime DateLimiteInscription { get; set; }
        public string Description { get; set; }
        public string CoverPhotoPath { get; set; }
        public int MaxParticipants { get; set; }
        public int MinParticipants { get; set; }
        public double PrixBillet { get; set; }
        public int IdOrganisateur { get; set; }
        public int IdAlbum { get; set; }

    }
}
