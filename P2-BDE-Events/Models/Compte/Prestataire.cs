using System;

namespace P2_BDE_Events.Models.Compte
{
    public class Prestataire : Compte
    {
        public int Id { get; set; }
        public string Adresse { get; set; }
        public string ZoneActivite { get; set; }
        public DateTime HeureDebutActivite { get; set; }
        public DateTime HeureFinActivite { get; set; }
        public string RaisonSocial { get; set; }
        public int NumeroSiret { get; set; }
        public string Presentation { get; set; }
    }
}
