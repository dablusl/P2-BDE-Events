using System.ComponentModel.DataAnnotations;
using System;

namespace P2_BDE_Events.ViewModels
{
    public class Etape1ViewModel
    {
        [Required(ErrorMessage = "Le champ Titre est obligatoire.")]
        [Display(Name = "Titre de l'événement")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Le champ Date est obligatoire.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de l'événement")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Le champ Theme est obligatoire.")]
        [Display(Name = "Thème de l'événement")]
        public string Theme { get; set; }
    }

}