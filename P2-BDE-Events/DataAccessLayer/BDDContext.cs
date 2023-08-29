using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Models.Stats;
using P2_BDE_Events.Services.Comptes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace P2_BDE_Events.DataAccessLayer
{
    public class BDDContext : DbContext
    {
        public DbSet<Organisateur> Organisateurs { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Prestataire> Prestataires { get; set; }
        public DbSet<Prestation> Prestations { get; set; }
        public DbSet<FacturePrestation> FacturePrestations { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Litige> Litiges { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<CommentaireEvenement> CommentaireEvenements { get; set; }
        public DbSet<CommentairePhoto> CommentairePhotos { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Avis> AvisUtilisateur { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<LigneEvenement> LignesEvenement { get; set; }
        public DbSet<Reserver> Reservations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPassword = Environment.GetEnvironmentVariable("BDE_EVENT_BDD_PASS");
            string connectionString = $"server=localhost;user id=root;password={dbPassword};database=P2_BDE_Events";
            optionsBuilder.UseMySql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reserver>()
                .HasOne(r => r.Participant)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.ParticipantId);

            modelBuilder.Entity<Reserver>()
                .HasOne(r => r.Evenement)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.EvenementId);
        }
        //Initiation différents comptes
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            //Compte Admin
            var adminCompte = new Compte
            {
                Id = 1,
                Email = "AdminAlpha",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Administrateur",
                Prenom = "Pierre",
                Nom = "Dupont",
                NumeroTelephone = "010101010101",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };
            this.Comptes.Add(adminCompte);
            this.SaveChanges();

            // Associer l'administrateur au compte
            var administrateur = new Administrateur
            {
                Id = 1,
                Compte = adminCompte,
            };
            this.Administrateurs.Add(administrateur);
            this.SaveChanges();


            //creation prestataire Compte
            var prestataireCompte5 = new Compte
            {
                Id = 5,
                Email = "Presta5",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Prestataire",
                Prenom = "Pierre",
                Nom = "Dupont",
                NumeroTelephone = "010101010101",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            var prestataire5 = new Prestataire
            {
                Id = 5,
                Compte = prestataireCompte5,
                RaisonSocial = "Isitest"
            };

            this.Prestations.AddRange(
         new Prestation
         {
             Id = 1,
             Titre = "Une salle pour vos evenement",
             Type = Models.Prestations.Enums.TypeDePrestation.SALLE,
             CapaciteMax = 10,
             Tarif = 100,
             Calendrier = "Du 1er au 5 août",
             Livraison = true,
             Description = "Une description de la prestation",
             Etat = EtatDePrestation.EnAttenteDeValidation,
             Prestataire = prestataire5
         },
            new Prestation
            {
                Id = 2,
                Titre = "Votre Dj",
                Type = Models.Prestations.Enums.TypeDePrestation.DJ,
                CapaciteMax = 50,
                Tarif = 200,
                Calendrier = "Tout août",
                Livraison = true,
                Description = "Dj pour vos soirée",
                Etat = EtatDePrestation.EnCours,
                Prestataire = prestataire5
            },
             new Prestation
             {
                 Id = 3,
                 Titre = "Tratieur pour vos papille",
                 Type = Models.Prestations.Enums.TypeDePrestation.TRAITEUR,
                 CapaciteMax = 300,
                 Tarif = 1000,
                 Calendrier = "3eme weekend du mois",
                 Livraison = true,
                 Description = "Traiteur spécialisé en calamar",
                 Etat = EtatDePrestation.Annulee,
                 Prestataire = prestataire5
             });



            //Compte Participant
            var Participant1 = new Compte
            {
                Id = 2,
                Email = "Participant1",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Participant",
                Prenom = "Jon",
                Nom = "LeFetar",
                NumeroTelephone = "010206080901",
            };
            this.Comptes.Add(Participant1);
            this.SaveChanges();

            // Associer le participant au compte
            var participant = new Participant
            {
                Id = 1,
                Compte = Participant1,
                NomBDE = "Paris12-ECO-BDE",
                Universite = "Paris12"
            };
            this.Participants.Add(participant);
            this.SaveChanges();
            
            //Compte Orga
            var Orga1 = new Compte
            {
                Id = 3,
                Email = "Orga1",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Organisateur",
                Prenom = "Juan Carlos",
                Nom = "L'Organisé",
                NumeroTelephone = "01020645945",
            };
            this.Comptes.Add(Orga1);
            this.SaveChanges();

            // Associer le participant au compte
            var orgaParticip = new Participant
            {
                Id = 2,
                Compte = Orga1,
                NomBDE = "Paris12-ECO-BDE",
                Universite = "Paris12"

            };
            var organisateur = new Organisateur
            {
                Id = 1,
                Participant = orgaParticip,
                FonctionBDE = "Directeur"
            };
            this.Organisateurs.Add(organisateur);
            this.SaveChanges();

            ////Compte Participant
            var Presta1 = new Compte
            {
                Id = 4,
                Email = "Presta1",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Prestataire",
                Prenom = "Luke",
                Nom = "LePresta",
                NumeroTelephone = "0102548974",
            };
            this.Comptes.Add(Presta1);
            this.SaveChanges();

            // Associer le presta au compte
            var prestat1 = new Prestataire
            {
                Id = 1,
                Compte = Presta1,
                RaisonSocial = "OnFaiTout",
                NumeroSiret = 124598787,
                ZoneActivite = "Ile De France",
                TypeActivite = "Lieu de réception",
                Presentation = "Chez OnFaiTout, on fait tout pour vous rendre heureux"

            };
            this.Prestataires.Add(prestat1);
            this.SaveChanges();

            this.Evenements.Add(
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
                    Organisateur = organisateur,
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
                    Organisateur = organisateur,
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
                    Organisateur = organisateur,
                },
                new Evenement
                {
                    Id = 0,
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
                    Organisateur = organisateur,
                }
                );
            /*
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
                });*/


            /*
            this.FacturePrestations.AddRange(
                 new FacturePrestation

                 {
                     Id = 1,
                     NumeroFacture = 20230101,
                     Date = new DateTime(2023, 01, 01),
                     MontantHT = 1000,
                     PrestationId = 3 
                 },
                 new FacturePrestation
                 {
                     Id = 2,
                     NumeroFacture = 20230201,
                     Date = new DateTime(2023, 02, 01),
                     MontantHT = 2000,
                     IdPrestation = 1
                 },
                 new FacturePrestation
                 {
                     Id = 3,
                     NumeroFacture = 20230506,
                     Date = new DateTime(2023, 05, 06),
                     MontantHT = 500,
                     IdPrestation = 2
                 },
                 new FacturePrestation
                 {
                     Id = 4,
                     NumeroFacture = 20231011,
                     Date = new DateTime(2023, 10, 11),
                     MontantHT = 3000,
                     IdPrestation = 3
                 },
                 new FacturePrestation
                 {
                     Id = 5,
                     NumeroFacture = 20230706,
                     Date = new DateTime(2023, 07, 06),
                     MontantHT = 5000,
                     IdPrestation = 1
                 });
            */
            this.SaveChanges();

        }
    }

}
