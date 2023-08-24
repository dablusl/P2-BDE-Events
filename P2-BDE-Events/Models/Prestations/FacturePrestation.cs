using System;

namespace P2_BDE_Events.Models.Prestations
{
    public class FacturePrestation
    {
        public int IdFacture { get; set; }
        public int NumeroFacture { get; set; }
        public DateTime Date { get; set; }
        public decimal MontantHT { get; set; }
        public int IdPrestation { get; set; }
        public Prestation Prestation { get; set; }
    }
}
