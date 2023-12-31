﻿using P2_BDE_Events.Models.Prestations;
using System;
using System.Collections.Generic;

namespace P2_BDE_Events.Models.Comptes
{
    public class Prestataire
    {

        public int Id { get; set; }
        public string Adresse { get; set; }
        public string RaisonSocial { get; set; }
        public int NumeroSiret { get; set; }
        public string ZoneActivite { get; set; }
        public string TypeActivite { get; set; }
        public DateTime HeureDebutActivite { get; set; }
        public DateTime HeureFinActivite { get; set; }
        public string Presentation { get; set; }
        public virtual ICollection<Prestation> Prestations { get; set; }
        public int CompteId { get; set; }
        public virtual Compte Compte { get; set; }
    }
}
