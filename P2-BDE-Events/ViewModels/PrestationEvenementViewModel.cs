using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class PrestationEvenementViewModel
    {
        public List<LigneEvenement> Lignes { get; set; }
        public Evenement Evenement { get; set; }
    }
}
