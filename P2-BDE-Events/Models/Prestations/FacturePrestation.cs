using System;

namespace P2_BDE_Events.Models.Prestations
{
    public class FacturePrestation
    {
        public int NumeroFacture { get; set; }
        public DateTime Date { get; set; }
        public decimal MontantHT { get; set; }
        public virtual Prestation Prestation{ get; set; }
    }
}
