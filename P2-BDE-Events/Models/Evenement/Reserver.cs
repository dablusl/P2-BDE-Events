using P2_BDE_Events.Models.Comptes;
using System;

namespace P2_BDE_Events.Models.Evenement
{
    public class Reserver
    {
        public int Id { get; set; }
        public DateTime DateReservation { get; set; }
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }
        public int EvenementId { get; set; }
        public virtual Evenement Evenement { get; set; }
    }
}
