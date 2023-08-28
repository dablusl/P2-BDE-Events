using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class LigneEvenement
    {
        public virtual Prestation Prestation { get; set; }

        [Key]
        [Required]
        public virtual Evenement Evenement { get; set; }

        [Key]
        [Required]
        public TypeDePrestation Type { get; set; }

    }
}
