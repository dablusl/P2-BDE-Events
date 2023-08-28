namespace P2_BDE_Events.Models.Comptes
{
    public class Participant
    {
        public int Id { get; set; }
        public virtual Compte Compte { get; set; }
        public string NomBDE { get; set; }
        public string Universite { get; set; }

    }
}
