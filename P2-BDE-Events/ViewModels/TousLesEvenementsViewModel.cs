using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class TousLesEvenementsViewModel
    {
        public List<Evenement> Evenements { get; set; }
        public Organisateur Organisateur { get; set;}    }
}
