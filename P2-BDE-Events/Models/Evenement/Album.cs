using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class Album
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int EvenementId { get; set; }
        public virtual Evenement Evenement { get; set; }
    }
}
