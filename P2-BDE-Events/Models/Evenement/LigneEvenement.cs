using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class LigneEvenement
    {
        public int Id { get; set; }
        public virtual Prestation Prestation { get; set; }
        public TypeDePrestation Type { get; set; }
        public TypeDeLocation Location { get; set; }
        public double TarifProposee { get; set; }
        public virtual ICollection<PropositionPrestation> Propositions { get; set; }
        public int EvenementId { get; set; }
        public virtual Evenement Evenement { get; set; } 
    }
}
