using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations.Enums;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace P2_BDE_Events.ViewModels
{
    public class AppelDoffreViewModel
    {
        public List<TypeDePrestation> Types { get; set; }
        public List<Evenement> EvenementsEnAppelDoffre { get; set; }
    }
}
