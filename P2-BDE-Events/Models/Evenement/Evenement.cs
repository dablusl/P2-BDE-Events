using P2_BDE_Events.Models.Evenement.Enums;
using System;
using System.Collections.Generic;

namespace P2_BDE_Events.Models.Evenement
{
    public class Evenement
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public EtatEvenement Etat { get; set; }
        public TypeEvenement Type { get; set; }
        public DateTime CreeLe { get; set; }
        public DateTime DateEvenement { get; set; } // Ajoutez la propriété pour la date de l'événement
        public DateTime DateLimiteInscription { get; set; }
        public string Description { get; set; }
        public string CoverPhotoPath { get; set; } // Ajoutez la propriété pour l'affiche de l'événement
        public int MaxParticipants { get; set; }
        public int MinParticipants { get; set; }
        public int NbReservations { get; set; }
        public int NbParticipants { get; set; }
        public double PrixBillet { get; set; }
        public int IdOrganisateur { get; set; }
        public int IdAlbum { get; set; }

    }
}
