using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Models.Evenement;

namespace P2_BDE_Events.DataAccessLayer
{
    public class BDDContext : DbContext
    {
        public DbSet<Organisateur> Organisateurs { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Prestataire> Prestataires { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Litige> Litiges { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<CommentaireEvenement> CommentaireEvenements { get; set; }  
        public DbSet<CommentairePhoto> CommentairePhotos { get; set; }
        public DbSet<Photo> Photos { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=5.M~{swM6^QOsgm;database=P2_BDE_Events");
        }
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Organisateurs.AddRange(
                new Organisateur
                {
                    Id = 1,
                    Email = "orga1@organisateur.com",
                    MotDePasse = "rrrrr",
                    Prenom = "Pierre",
                    Nom = "Dupont",
                    NumeroTelephone = "010101010101"
                }               
            );
           
            this.SaveChanges();
        }
    }
}
