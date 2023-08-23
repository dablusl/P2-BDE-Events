using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.ViewModels
{
    public class Etape2ViewModel
    {
        [Required(ErrorMessage = "Le champ Nombre de participants souhaité est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez entrer un nombre valide.")]
        [Display(Name = "Nombre de participants souhaité (max)")]
        public int NombreParticipants { get; set; }
    }

}