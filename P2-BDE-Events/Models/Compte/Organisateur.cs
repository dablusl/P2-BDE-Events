using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_BDE_Events.Models.Compte
{
    public class Organisateur
    {
        public int Id { get; set; }

        public virtual Participant Participant { get; set; }
        public string FonctionBDE { get; set; }

    }
 
}
