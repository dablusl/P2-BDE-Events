﻿using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using System.Collections.Generic;

namespace P2_BDE_Events.ViewModels
{
    public class PropositionsPrestationViewModel
    {
        public LigneEvenement Ligne { get; set; }
        public List<PropositionPrestation> Propositions { get; set; }

        public Evenement Evenement { get; set; }
        public int LigneId { get; set; }
        public int PropositionID { get; set; }
        public int EvenementID { get; set; }
    }
}
