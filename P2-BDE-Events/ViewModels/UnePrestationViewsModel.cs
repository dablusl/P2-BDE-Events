using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Prestations;

namespace P2_BDE_Events.ViewModels
{
    public class UnePrestationViewsModel
    {
        public Prestation prestation { get; set; }

        public Prestataire prestataire { get; set; }
    }
}
