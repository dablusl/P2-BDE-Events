using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class EvenementPrestaViewModel
    {
        public List<Evenement> Evenements { get; set; }
        public int IdPrestataire { get; set; }
    }
}
