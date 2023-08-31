using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Models.Stats;
using P2_BDE_Events.Services.Comptes;

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
        public DbSet<PropositionPrestation> Propositions { get; set; }


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
                Prenom = "Philipe",
                Nom = "Xiu",
                NumeroTelephone = "0145789698",
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
             Type = TypeDePrestation.SALLE,
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
                Type = TypeDePrestation.DJ,
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
                 Type = TypeDePrestation.TRAITEUR,
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
                NumeroTelephone = "0102060809",
            };
            this.Comptes.Add(Participant1);
            this.SaveChanges();

            var participant = new Participant
            {
                Id = 1,
                Compte = Participant1,
                NomBDE = "Paris12-ECO-BDE",
                Universite = "Paris12"
            };
            this.Participants.Add(participant);
            this.SaveChanges();

            //Compte Participant 2
            var Participant2 = new Compte
            {
                Id = 6,
                Email = "Participant2",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Participant",
                Prenom = "Momo",
                Nom = "Ali",
                NumeroTelephone = "0105097841",
            };
            this.Comptes.Add(Participant2);
            this.SaveChanges();

            var particip2 = new Participant
            {
                Id = 3,
                Compte = Participant2,
                NomBDE = "Paris12-ECO-BDE",
                Universite = "Paris12"
            };
            this.Participants.Add(particip2);
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

            ////Compte Presta
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

            //prestations

            Compte cPrestataire10 = new Compte
            {
                Id = 10,
                Email = "bBieres@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Prestataire",
                Prenom = "Charles",
                Nom = "DeVin",
                NumeroTelephone = "+33674544676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cPrestataire11 = new Compte
            {
                Id = 11,
                Email = "pizzasLumberjack@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Prestataire",
                Prenom = "Xavier",
                Nom = "Brasseur",
                NumeroTelephone = "+33674555576",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cPrestataire12 = new Compte
            {
                Id = 12,
                Email = "camille@stereolux.fr",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Prestataire",
                Prenom = "Camille",
                Nom = "Durant",
                NumeroTelephone = "+33689544676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cPrestataire13 = new Compte
            {
                Id = 13,
                Email = "j.elbeux@crit.fr",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Prestataire",
                Prenom = "Elodie",
                Nom = "Van Trop",
                NumeroTelephone = "+33774544676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cPrestataire14 = new Compte
            {
                Id = 14,
                Email = "daDj@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Prestataire",
                Prenom = "Katia",
                Nom = "Clichy",
                NumeroTelephone = "+33679844676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            this.Comptes.AddRange(
                cPrestataire11,
                cPrestataire10,
                cPrestataire12,
                cPrestataire13,
                cPrestataire14
                );
            this.SaveChanges();

            Prestataire pPrestataire11 = new Prestataire
            {
                Id = 11,
                Compte = cPrestataire11,
                RaisonSocial = "Pizzas Lumberjack"
            };

            Prestataire pPrestataire10 = new Prestataire
            {
                Id = 10,
                Compte = cPrestataire10,
                RaisonSocial = "B Bar à Bieres"
            };
            Prestataire pPrestataire12 = new Prestataire
            {
                Id = 12,
                Compte = cPrestataire12,
                RaisonSocial = "Stereolux"
            };

            Prestataire pPrestataire13 = new Prestataire
            {
                Id = 13,
                Compte = cPrestataire13,
                RaisonSocial = "Crit Interim Securite"
            };

            Prestataire pPrestataire14 = new Prestataire
            {
                Id = 14,
                Compte = cPrestataire14,
                RaisonSocial = "DJ Blaz"
            };

            this.Prestataires.AddRange(
                pPrestataire10,
                pPrestataire11,
                pPrestataire12,
                pPrestataire13,
                pPrestataire14
                );
            this.SaveChanges();

            Prestation presta10 = new Prestation
            {
                Id = 10,
                Type = TypeDePrestation.BAR,
                Description = "Bar à bières, large sélection belge et allemande",
                CapaciteMax = 99999999,
                Prestataire = pPrestataire10,
                Tarif = 100,
            };

            Prestation presta11 = new Prestation
            {
                Id = 11,
                Type = TypeDePrestation.SALLE,
                Description = "Bar à bières, large sélection belge et allemande",
                CapaciteMax = 80,
                Prestataire = pPrestataire10,
                Tarif = 100,
            };
            Prestation presta12 = new Prestation
            {
                Id = 12,
                Type = TypeDePrestation.TRAITEUR,
                Description = "Pizza artisanales",
                CapaciteMax = 120,
                Prestataire = pPrestataire11,
                Tarif = 120,
            };
            Prestation presta13 = new Prestation
            {
                Id = 13,
                Type = TypeDePrestation.SALLE,
                Description = "Salle MAXI - Salle Concert",
                CapaciteMax = 1000,
                Prestataire = pPrestataire12,
                Tarif = 1000,
            };
            Prestation presta14 = new Prestation
            {
                Id = 14,
                Type = TypeDePrestation.SALLE,
                Description = "Salle MICRO - Salle Concert",
                CapaciteMax = 500,
                Prestataire = pPrestataire12,
                Tarif = 1000,
            };
            Prestation presta15 = new Prestation
            {
                Id = 15,
                Type = TypeDePrestation.BAR,
                Description = "Bière Pression Locale, plus d'info inbox",
                CapaciteMax = 1000,
                Prestataire = pPrestataire12,
                Tarif = 1000,

            };
            Prestation presta16 = new Prestation
            {
                Id = 16,
                Type = TypeDePrestation.SECURITE,
                Description = "Securite moyen evenements",
                CapaciteMax = 1000,
                Prestataire = pPrestataire13,
                Tarif = 1000,
            };
            Prestation presta17 = new Prestation
            {
                Id = 17,
                Type = TypeDePrestation.DJ,
                Description = "DJ",
                CapaciteMax = 99999,
                Prestataire = pPrestataire13,
                Tarif = 1000,
            };


            this.Prestations.AddRange(
                presta10,
                presta11,
                presta12,
                presta13,
                presta14,
                presta15,
                presta16,
                presta17
                );
            this.SaveChanges();
            //evenements

            Compte cOrga10 = new Compte
            {
                Id = 15,
                Email = "michael.cera@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Organisateur",
                Prenom = "Michael",
                Nom = "Cera",
                NumeroTelephone = "+33674544676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cOrga11 = new Compte
            {
                Id = 16,
                Email = "j.balie@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Organisateur",
                Prenom = "Jeanne",
                Nom = "Bali",
                NumeroTelephone = "+33674544676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cOrga12 = new Compte
            {
                Id = 17,
                Email = "m.sako@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Organisateur",
                Prenom = "Moise",
                Nom = "Sako",
                NumeroTelephone = "+33674544676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };
            this.Comptes.AddRange(
                cOrga10,
                cOrga11,
                cOrga12
                );
            this.SaveChanges();

            Participant pOrga10 = new Participant
            {
                Id = 10,
                Compte = cOrga10,
                NomBDE = "Paris-Descartes-Droit-BDE",
                Universite = "Paris-Descartes"
            };

            Participant pOrga11 = new Participant
            {
                Id = 11,
                Compte = cOrga11,
                NomBDE = "ParisSorbonne-Lettres-BDE",
                Universite = "Paris-Sorbonne"
            };

            Participant pOrga12 = new Participant
            {
                Id = 12,
                Compte = cOrga12,
                NomBDE = "HEC-ECO-BDE",
                Universite = "HEC"
            };
            this.Participants.AddRange(
                pOrga10,
                pOrga11,
                pOrga12
                );
            this.SaveChanges();

            Organisateur oOrga10 = new Organisateur
            {
                Id = 10,
                Participant = pOrga10,
                FonctionBDE = "Directeur"
            };

            Organisateur oOrga11 = new Organisateur
            {
                Id = 11,
                Participant = pOrga11,
                FonctionBDE = "Directeur"
            };

            Organisateur oOrga12 = new Organisateur
            {
                Id = 12,
                Participant = pOrga12,
                FonctionBDE = "Directeur"
            };
            this.Organisateurs.AddRange(
                oOrga10,
                oOrga11,
                oOrga12
                );
            this.SaveChanges();
            //------------------
            Compte cParticip3 = new Compte
            {
                Id = 18,
                Email = "will@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Participant",
                Prenom = "Will",
                Nom = "LaFouine",
                NumeroTelephone = "+33674544786",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cParticip4 = new Compte
            {
                Id = 19,
                Email = "david@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Participant",
                Prenom = "David",
                Nom = "Leclerc",
                NumeroTelephone = "+33675644676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };

            Compte cParticip5 = new Compte
            {
                Id = 20,
                Email = "salomon@gmail.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Participant",
                Prenom = "Salomon",
                Nom = "Ben Soussan",
                NumeroTelephone = "+33674744676",
                PhotoProfilePath = "/images/utilisateurs/41752-125261.jpg"
            };
            this.Comptes.AddRange(
                cParticip3,
                cParticip4,
                cParticip5
                );
            this.SaveChanges();

            Participant pParticip3 = new Participant
            {
                Id = 13,
                Compte = cParticip3,
                NomBDE = "Paris-Descartes-Droit-BDE",
                Universite = "Paris-Descartes"
            };

            Participant pParticip4 = new Participant
            {
                Id = 14,
                Compte = cParticip4,
                NomBDE = "ParisSorbonne-Lettres-BDE",
                Universite = "Paris-Sorbonne"
            };

            Participant pParticip5 = new Participant
            {
                Id = 15,
                Compte = cParticip5,
                NomBDE = "HEC-ECO-BDE",
                Universite = "HEC"
            };
            this.Participants.AddRange(
                pParticip3,
                pParticip4,
                pParticip5
                );
            this.SaveChanges();
            //-----------
            Evenement evenement1 = new Evenement
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
                NbParticipants = 1,
                PrixBillet = 8.5,
                Organisateur = organisateur,
            };
            Evenement evenement2 = new Evenement
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
                NbParticipants = 1,
                PrixBillet = 10,
                Organisateur = oOrga10,
            };
            Evenement evenement3 = new Evenement
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
                NbParticipants = 1,
                PrixBillet = 5,
                Organisateur = organisateur,
            };
            Evenement evenement4 = new Evenement
            {
                Id = 4,
                Titre = "Concert Indie",
                Etat = EtatEvenement.OUVERT,
                Type = TypeEvenement.CONCERT,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Ramenez vos instruments et vos cordes vocales",
                CoverPhotoPath = "/images/evenement/18.10.15_PeachPit_PeachPit.png",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 5,
                Organisateur = organisateur,
            };

            Evenement evenement5 = new Evenement
            {
                Id = 5,
                Titre = "Soiree Jeux de Societes & Trivia",
                Etat = EtatEvenement.OUVERT,
                Type = TypeEvenement.JEUXDESOCIETE,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Ramenez vos instruments et vos cordes vocales",
                CoverPhotoPath = "/images/evenement/3/top-karaoke-songs.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 5,
                Organisateur = oOrga11,
            };

            Evenement evenement6 = new Evenement
            {
                Id = 6,
                Titre = "Petanque d'Intégration",
                Etat = EtatEvenement.OUVERT,
                Type = TypeEvenement.SPORT,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Ramenez vos instruments et vos cordes vocales",
                CoverPhotoPath = "/images/evenement/3/top-karaoke-songs.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 5,
                Organisateur = oOrga11,
            };

            Evenement evenement7 = new Evenement
            {
                Id = 7,
                Titre = "Soiree Horror Cinema Slasher",
                Etat = EtatEvenement.OUVERT,
                Type = TypeEvenement.CINEMA,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Pop corn non inclus",
                CoverPhotoPath = "/images/evenement/3/top-karaoke-songs.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 5,
                Organisateur = oOrga11,
            };

            Evenement evenement8 = new Evenement
            {
                Id = 8,
                Titre = "Repas de Noël + Santa Mega Secret",
                Etat = EtatEvenement.OUVERT,
                Type = TypeEvenement.REPAS,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Pop corn non inclus",
                CoverPhotoPath = "/images/evenement/3/top-karaoke-songs.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 5,
                Organisateur = oOrga10,
            };

            Evenement evenement9 = new Evenement
            {
                Id = 9,
                Titre = "2024 new year new me",
                Etat = EtatEvenement.OUVERT,
                Type = TypeEvenement.SOIREE,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Pop corn non inclus",
                CoverPhotoPath = "/images/evenement/3/top-karaoke-songs.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 5,
                Organisateur = oOrga10,
            };

            Evenement evenement10 = new Evenement
            {
                Id = 10,
                Titre = "Picnic et Animaux de Compagnie",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.PLEINE_AIRE,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 31, 21, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Pop corn non inclus",
                CoverPhotoPath = "/images/evenement/3/top-karaoke-songs.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 0,
                Organisateur = oOrga12,
            };

            this.Evenements.AddRange(
                evenement1,
                evenement2,
                evenement3,
                evenement4,
                evenement5,
                evenement6,
                evenement7,
                evenement8,
                evenement9,
                evenement10
                );
            this.SaveChanges();

            this.LignesEvenement.AddRange(
                new LigneEvenement
                {
                    Id= 1,
                    Evenement = evenement1,
                    Type = TypeDePrestation.BAR,
                    Prestation = presta10
                },
                new LigneEvenement
                {
                    Id = 2,
                    Evenement = evenement1,
                    Type = TypeDePrestation.SALLE,
                    Prestation = presta11
                },
                new LigneEvenement
                {
                    Id = 3,
                    Evenement = evenement1,
                    Type = TypeDePrestation.TRAITEUR,
                    Prestation = presta12
                },
                new LigneEvenement
                {
                    Id = 4,
                    Evenement = evenement2,
                    Type = TypeDePrestation.SALLE,
                    Prestation = presta14
                },
                new LigneEvenement
                {
                    Id = 5,
                    Evenement = evenement3,
                    Type = TypeDePrestation.SECURITE,
                    Prestation = presta16
                },
                new LigneEvenement
                {
                    Id = 6,
                    Evenement = evenement2,
                    Type = TypeDePrestation.DJ,
                    Prestation = presta17
                },
                new LigneEvenement
                {
                    Id = 7,
                    Evenement = evenement4,
                    Type = TypeDePrestation.SALLE,
                },
                new LigneEvenement
                {
                    Id = 8,
                    Evenement = evenement4,
                    Type = TypeDePrestation.BAR,
                },
                new LigneEvenement
                {
                    Id = 9,
                    Evenement = evenement5,
                    Type = TypeDePrestation.BAR,
                },
                new LigneEvenement
                {
                    Id = 10,
                    Evenement = evenement5,
                    Type = TypeDePrestation.SALLE,
                },
                new LigneEvenement
                {
                    Id = 11,
                    Evenement = evenement6,
                    Type = TypeDePrestation.PHOTOGRAPHIE,
                },
                new LigneEvenement
                {
                    Id = 12,
                    Evenement = evenement7,
                    Type = TypeDePrestation.SALLE,
                },
                new LigneEvenement
                {
                    Id = 13,
                    Evenement = evenement7,
                    Type = TypeDePrestation.TRAITEUR,
                },
                new LigneEvenement
                {
                    Id = 14,
                    Evenement = evenement7,
                    Type = TypeDePrestation.BAR,
                },
                new LigneEvenement
                {
                    Id = 15,
                    Evenement = evenement7,
                    Type = TypeDePrestation.LOCATION,
                },
                new LigneEvenement
                {
                    Id = 16,
                    Evenement = evenement8,
                    Type = TypeDePrestation.TRAITEUR,
                },
                new LigneEvenement
                {
                    Id = 17,
                    Evenement = evenement8,
                    Type = TypeDePrestation.SALLE,
                },
                new LigneEvenement
                {
                    Id = 18,
                    Evenement = evenement9,
                    Type = TypeDePrestation.SALLE,
                },
                new LigneEvenement
                {
                    Id = 19,
                    Evenement = evenement9,
                    Type = TypeDePrestation.BAR,
                },
                new LigneEvenement
                {
                    Id = 20,
                    Evenement = evenement9,
                    Type = TypeDePrestation.DJ,
                },
                new LigneEvenement
                {
                    Id = 21,
                    Evenement = evenement9,
                    Type = TypeDePrestation.PHOTOGRAPHIE,
                },
                new LigneEvenement
                {
                    Id=22,
                    Evenement = evenement9,
                    Type = TypeDePrestation.LOCATION,
                    Location = TypeDeLocation.PHOTOMATON
                }
                );
            this.SaveChanges();

            this.Reservations.Add(new Reserver
            {
                DateReservation = DateTime.Now,
                Participant = pParticip3,
                Evenement = evenement1
            });

            this.Reservations.Add(new Reserver
            {
                DateReservation = DateTime.Now,
                Participant = pParticip4,
                Evenement = evenement2
            });

            this.Reservations.Add(new Reserver
            {
                DateReservation = DateTime.Now,
                Participant = pParticip5,
                Evenement = evenement5
            });

            this.SaveChanges();

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
