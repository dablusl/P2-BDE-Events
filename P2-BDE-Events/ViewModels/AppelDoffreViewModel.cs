using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace P2_BDE_Events.ViewModels
{
    public class AppelDoffreViewModel
    {
        public List<Evenement> EvenementsEnAppelDoffre { get; set; }
        public PropositionPrestation PropositionPrestation { get; set; }
        public List<TypeDePrestation> Types { get; set; }
        public List<PropositionPrestation> Propositions { get; set; }
        public LigneEvenement Ligne { get; set; }
        public Evenement EvenementInteresse { get; set; }
        public TypeDePrestation TypeInteresse { get; set; }
        public List<Prestation> PrestationsDuPrestataire { get; set; }

    }
}
