using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;

namespace P2_BDE_Events.Models.Evenement
{
    public class LigneEvenement
    {
        public virtual Prestation Prestation { get; set; }
        public virtual Evenement Evenement { get; set; }
        public TypeDePrestation Type { get; set; }

        public LigneEvenement(Evenement evenement, TypeDePrestation type)
        {
            Evenement = evenement;
            Type = type;
        }
    }
}
