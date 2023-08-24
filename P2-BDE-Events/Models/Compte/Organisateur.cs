using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_BDE_Events.Models.Compte
{
    public class Organisateur
    {
        public int Id { get; set; }
        public int CompteId { get; set; }
        public virtual Compte Compte { get; set; }
        public string NomBDE { get; set; }
        public string Universite { get; set; }
    }
 
}
