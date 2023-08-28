using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class LigneEvenement
    {
        public virtual Prestation Prestation { get; set; }
        [Key]
        public TypeDePrestation Type { get; set; }
        public int EvenementId { get; set; }
        [Key]
        public virtual Evenement Evenement { get; set; }
    }
}
