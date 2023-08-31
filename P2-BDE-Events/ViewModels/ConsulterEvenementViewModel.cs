using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class ConsulterEvenementViewModel
    {
        public Evenement Evenement { get; set; }
        public List<Participant> Participants { get; set; }
        public int NbParticipant { get; set; }

    }
}
