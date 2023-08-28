namespace P2_BDE_Events.Models.Comptes
{
    public class Organisateur
    {
        public int Id { get; set; }
        public virtual Participant Participant { get; set; }
        public string FonctionBDE { get; set; }

    }
}
