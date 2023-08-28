namespace P2_BDE_Events.Models.Comptes
{
    public class Administrateur
    {
        public int Id { get; set; }
        public int CompteId { get; set; }
        public virtual Compte Compte { get; set; }
    }
}
