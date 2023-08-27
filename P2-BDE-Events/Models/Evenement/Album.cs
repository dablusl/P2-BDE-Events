namespace P2_BDE_Events.Models.Evenement
{
    public class Album
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual Evenement Evenement { get; set; }

        public Album(Evenement evenement)
        {
            Evenement = evenement;
        }
    }
}
