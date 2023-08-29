namespace P2_BDE_Events.Models.Prestations
{
    public class PropositionPrestation
    {
        public int Id { get; set; }
        public int PrestationId { get; set; }
        public virtual Prestation Prestation { get; set; }
        public int EvenementId { get; set; }
        public virtual P2_BDE_Events.Models.Evenement.Evenement Evenement { get; set; }
    }
}
