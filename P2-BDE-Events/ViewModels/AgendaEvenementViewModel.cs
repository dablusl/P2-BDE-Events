using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class AgendaEvenementViewModel
    {
        public List<Evenement> Evenements { get; set; }
        public Participant Participant { get; set; }
        //public List<Participant> Participants { get; set; }


    }
}
