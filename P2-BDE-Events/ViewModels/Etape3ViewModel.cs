using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.ViewModels
{
    public class Etape3ViewModel
    {
        [Display(Name = "Alcool")]
        public bool Alcool { get; set; }

        [Display(Name = "Restauration")]
        public bool Restauration { get; set; }

        [Display(Name = "Sécurité")]
        public bool Securite { get; set; }

        [Display(Name = "Bar")]
        public bool Bar { get; set; }
    }

}