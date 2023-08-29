using System;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Prestations
{
    public class FacturePrestation
    {
        public int Id { get; set; }
        public int NumeroFacture { get; set; }
        public DateTime Date { get; set; }
        public decimal MontantHT { get; set; }

        public int PrestationId { get; set; }
        public Prestation Prestation { get; set; }
    }
}
