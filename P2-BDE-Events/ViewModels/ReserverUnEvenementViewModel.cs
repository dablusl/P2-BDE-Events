using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class ReserverUnEvenementViewModel
    {
        public Evenement Evenement { get; set; }
        public int NbParticipant { get; set; }
        public bool DejaReserve { get; set; }
    }
}
