namespace P2_BDE_Events.Models.Compte
{
    public class Organisateur
    {
        public int Id { get; set; }
        public virtual Compte Compte { get; set; }

        public string NomBDE { get; set; }
    }
 
}
