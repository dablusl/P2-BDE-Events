using P2_BDE_Events.Models.Evenement;

namespace P2_BDE_Events.Models.Prestations
{
    public class PropositionPrestation
    {
        public int Id { get; set; }
        public double TarifPropose { get; set; }
        public int PrestationId { get; set; }
        public virtual Prestation Prestation { get; set; }
        public int LigneEvenementId { get; set; }
        public virtual LigneEvenement LigneEvenement { get; set; }
    }
}
