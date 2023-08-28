using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class LigneEvenement
    {
        public int Id { get; set; }
        public virtual Prestation Prestation { get; set; }
        public virtual Evenement Evenement { get; set; }
        [Required]
        public TypeDePrestation Type { get; set; }

    }
}
