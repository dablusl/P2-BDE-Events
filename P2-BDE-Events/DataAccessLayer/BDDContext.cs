using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Models.Evenement;
using System;
using P2_BDE_Events.Models.Evenement.Enums;

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
            string dbPassword = Environment.GetEnvironmentVariable("BDE_EVENT_BDD_PASS");
            string connectionString = $"server=localhost;user id=root;password={dbPassword};database=P2_BDE_Events";
            optionsBuilder.UseMySql(connectionString);
        }

        //organisateurs, etudiants,
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Organisateurs.AddRange(
                new Organisateur
                {
                    Id = 1,
                    Email = "pierre.dupont@etu.univ-paris.com",
                    MotDePasse = "rrrrr",
                    Prenom = "Pierre",
                    Nom = "Dupont",
                    NumeroTelephone = "010101010101",
                    PhotoProfilePath = "../wwwroot/utilisateurs/41752-125261.jpg"
                },
                new Organisateur
                {
                    Id = 2,
                    Email = "michael.cera@etu.univ-nantes.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Michael",
                    Nom = "Cera",
                    NumeroTelephone = "+330712211221",
                    PhotoProfilePath = "../wwwroot/utilisateurs/5421c3ef22d1b.image.jpg"
                },
                new Organisateur
                {
                    Id = 3,
                    Email = "maybe.funke@etu.univ-nantes.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Maybe",
                    Nom = "Funke",
                    NumeroTelephone = "+330712211221",
                    PhotoProfilePath = "../wwwroot/utilisateurs/5e795e773cf12ae5e5ef36b6b9d40a2e.jpg"
                },
                new Organisateur
                {
                    Id = 4,
                    Email = "michael.scott@etu.univ-orleans.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Michael",
                    Nom = "Scott",
                    NumeroTelephone = "+330712211221",
                    PhotoProfilePath = "../wwwroot/utilisateurs/b44f223d3ae049249c7a00be21eb4c6a--ellie-kemper-unbreakable-kimmy-schmidt.jpg"
                }

            );
            this.Participants.AddRange(
                new Participant
                {
                    Id = 1,
                    Email = "gonzalo.benites@etu.univ-marseille.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Gonzalo",
                    Nom = "Benites",
                    NumeroTelephone = "+330712211221"
                },
                new Participant
                {
                    Id = 2,
                    Email = "maybe.funke@etu.univ-orleans.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Lucas",
                    Nom = "Dechaumet",
                    NumeroTelephone = "+330712211221"
                },
                new Participant
                {
                    Id = 3,
                    Email = "ezaiah.funke@etu.univ-orleans.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Ezaiah",
                    Nom = "Scott",
                    NumeroTelephone = "+330712211221"
                },
                new Participant
                {
                    Id = 4,
                    Email = "maybe.funke@etu.univ-orleans.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Thadé",
                    Nom = "Scott",
                    NumeroTelephone = "+330712211221"
                });
            this.Administrateurs.AddRange(
                new Administrateur
                {
                    Id = 1,
                    Email = "admin@BDE-Events.com",
                    MotDePasse = "1234",
                    Prenom = "Emmanuelle",
                    Nom = "Hollande",
                    NumeroTelephone = "+330712211221",
                    PhotoProfilePath = "../wwwroot/utilisateurs/Dwight-the-office.jpg"
                });
            this.Evenements.AddRange(
                new Evenement
                {
                    Id = 1,
                    Titre = "Beer Pong LEA vs DROIT",
                    Etat = EtatEvenement.PUBLIE,
                }) ;
            this.SaveChanges();
        }
    }
}
