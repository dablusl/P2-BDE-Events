using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Models.Evenement;
using System;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Models.Stats;

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
        public DbSet<Avis> AvisUtilisateur { get; set; } 

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
                    Id = 5,
                    Email = "pierre.dupont@etu.univ-paris.com",
                    MotDePasse = "rrrrr",
                    Prenom = "Pierre",
                    Nom = "Dupont",
                    NumeroTelephone = "010101010101",
                    PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
                },
                new Organisateur
                {
                    Id = 2,
                    Email = "michael.cera@etu.univ-nantes.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Michael",
                    Nom = "Cera",
                    NumeroTelephone = "+330712211221",
                    PhotoProfilePath = "/images/utilisateurs/5421c3ef22d1b.image.jpg"
                },
                new Organisateur
                {
                    Id = 3,
                    Email = "maybe.funke@etu.univ-nantes.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Maybe",
                    Nom = "Funke",
                    NumeroTelephone = "+330712211221",
                    PhotoProfilePath = "/images/utilisateurs/5e795e773cf12ae5e5ef36b6b9d40a2e.jpg"
                },
                new Organisateur
                {
                    Id = 4,
                    Email = "michael.scott@etu.univ-orleans.com",
                    MotDePasse = "123.Csq2.&",
                    Prenom = "Michael",
                    Nom = "Scott",
                    NumeroTelephone = "+330712211221",
                    PhotoProfilePath = "/images/utilisateurs/b44f223d3ae049249c7a00be21eb4c6a--ellie-kemper-unbreakable-kimmy-schmidt.jpg"
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
                    PhotoProfilePath = "/images/utilisateurs/Dwight-the-office.jpg"
                });
            this.Evenements.AddRange(
                new Evenement
                {
                    Id = 1,
                    Titre = "Beer-pong LEA vs DROIT",
                    Etat = EtatEvenement.PUBLIE,
                    Type = TypeEvenement.SOIREE,
                    CreeLe = new DateTime(2023, 09, 02, 10, 30, 20),
                    DateEvenement = new DateTime(2023, 10, 15, 20, 0, 0),
                    DateLimiteInscription = new DateTime(2023, 10, 15, 0, 0, 0),
                    Description = "Defend l'honneur de ta fac avec ton talent surhumain",
                    CoverPhotoPath = "/images/evenement/1/téléchargement (1).jpeg",
                    MaxParticipants = 100,
                    MinParticipants = 70,
                    NbReservations = 0,
                    NbParticipants = 0,
                    PrixBillet = 8.5,
                    IdOrganisateur = 1,
                },
                new Evenement
                {
                    Id = 2,
                    Titre = "BOOO BOOO BOOO Halloween",
                    Etat = EtatEvenement.PUBLIE,
                    Type = TypeEvenement.SOIREE,
                    CreeLe = new DateTime(2023, 09, 10, 20, 30, 20),
                    DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                    DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                    Description = "Soirée faits moi très peur et concours de costumes",
                    CoverPhotoPath = "/images/evenement/5/HEX-HP-344199-site-080823-4x3.jpeg",
                    MaxParticipants = 200,
                    MinParticipants = 120,
                    NbReservations = 0,
                    NbParticipants = 0,
                    PrixBillet = 10,
                    IdOrganisateur = 2,
                },
                new Evenement
                {
                    Id = 3,
                    Titre = "Karaoke Jam Sesh Beaux Arts",
                    Etat = EtatEvenement.PUBLIE,
                    Type = TypeEvenement.SOIREE,
                    CreeLe = new DateTime(2023, 09, 10, 20, 30, 20),
                    DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                    DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                    Description = "Ramenez vos instruments et vos cordes vocales",
                    CoverPhotoPath = "/images/evenement/3/top-karaoke-songs.jpg",
                    MaxParticipants = 80,
                    MinParticipants = 50,
                    NbReservations = 0,
                    NbParticipants = 0,
                    PrixBillet = 5,
                    IdOrganisateur = 3,
                }
                );
            this.AvisUtilisateur.AddRange(
                new Avis
                {
                    Id = 1,
                    Titre = "Super Plateforme<3",
                    Contenu = "L'organisation devenements n'a jamais ete balbalbalba",
                    PublieLe = new DateTime(2023, 07, 02, 22, 10, 12),
                    Notation = 5,
                    IdAuthor = 2,
                },
                new Avis
                {
                    Id = 2,
                    Titre = "BDE Events à sauvé mon business",
                    Contenu = "Une croissance de 500% par rapport à l'année dernière",
                    PublieLe = new DateTime(2023, 08, 01, 12, 1, 2),
                    Notation = 5,
                    IdAuthor = 3,
                },
                new Avis
                {
                    Id = 3,
                    Titre = "IN CROY ABLE",
                    Contenu = "Plaisir d'avoir travailler avec vous les gars",
                    PublieLe = new DateTime(2023, 05, 01, 14, 1, 2),
                    Notation = 4.5,
                    IdAuthor = 4,
                },
                new Avis
                {
                    Id = 4,
                    Titre = "Concept Super",
                    Contenu = "Plaisir d'avoir travailler avec vous les garsx2",
                    PublieLe = new DateTime(2023, 05, 01, 14, 1, 2),
                    Notation = 4.5,
                    IdAuthor = 5,
                });
            this.SaveChanges();
        }
    }
}
