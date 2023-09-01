using P2_BDE_Events.Models.Prestations;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class ListePrestationsViewModel
    {
        public List<Prestation> Prestations { get; set; }
        public int IdPrestataire { get; set; }
    }
}