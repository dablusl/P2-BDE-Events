using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Models.Stats;
using P2_BDE_Events.Services.Comptes;
using System.Threading;

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
                Email = "adminalpha@bdeevents.com",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Administrateur",
                Prenom = "Pierre",
                Nom = "Dupont",
                NumeroTelephone = "0632655289",
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
                NumeroTelephone = "0145889698",
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
             Calendrier = DateTime.Today,
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
                Calendrier = DateTime.Today,
                Livraison = true,
                Description = "Dj pour vos soirée",
                Etat = EtatDePrestation.EnCours,
                Prestataire = prestataire5
            },
             new Prestation
             {
                 Id = 3,
                 Titre = "Traiteur pour vos papille",
                 Type = TypeDePrestation.TRAITEUR,
                 CapaciteMax = 300,
                 Tarif = 1000,
                 Calendrier = DateTime.Today,
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
            //-----
            var Orga2 = new Compte
            {
                Id = 121,
                Email = "jcdm@paris12.fr",
                MotDePasse = CompteService.EncodeMD5("rrrrr"),
                Profil = "Organisateur",
                Prenom = "Juan Carlos",
                Nom = "Del Mar",
                NumeroTelephone = "0652458796",
            };
            this.Comptes.Add(Orga2);
            this.SaveChanges();

            var orga2Particip = new Participant
            {
                Id = 116,
                Compte = Orga2,
                NomBDE = "Paris12-ECO-BDE",
                Universite = "Paris12"

            };
            var orgaP12 = new Organisateur
            {
                Id = 2,
                Participant = orga2Particip,
                FonctionBDE = "Responsable"
            };
            this.Organisateurs.Add(orgaP12);
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

            //prestataires

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
                Titre = "Bar à bières",
                Description = "Bar à bières, large sélection belge et allemande",
                CapaciteMax = 80,
                Prestataire = pPrestataire10,
                Tarif = 1000,
            };

            Prestation presta11 = new Prestation
            {
                Id = 11,
                Type = TypeDePrestation.SALLE,
                Titre = "Salle de réception",
                Description = "Salle de réception privative. Sono, tables et chaises disponibles à la demande",
                CapaciteMax = 180,
                Prestataire = pPrestataire10,
                Tarif = 1800,
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
                Titre = "Salle MAXI",
                Description = "Salle Concert",
                CapaciteMax = 1000,
                Prestataire = pPrestataire12,
                Tarif = 1000,
            };
            Prestation presta14 = new Prestation
            {
                Id = 14,
                Type = TypeDePrestation.SALLE,
                Titre = "Salle MICRO",
                Description = "Salle Concert",
                CapaciteMax = 500,
                Prestataire = pPrestataire12,
                Tarif = 1000,
            };
            Prestation presta15 = new Prestation
            {
                Id = 15,
                Type = TypeDePrestation.BAR,
                Titre = "Bières Locales",
                Description = "plus d'info inbox pour une offre plus detaille",
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
                Email = "m.cera@paris-descartes.fr",
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
                Email = "j.balie@paris-sorbonne.fr",
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
                Email = "m.sako@hec.fr",
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
                Email = "will@paris-descartes.fr",
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
                Email = "d.leclerc@paris-sorbonne.fr",
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
                Email = "salomon@hec.fr",
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
                Titre = "Back to School",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.SOIREE,
                CreeLe = new DateTime(2023, 06, 4, 10, 30, 20),
                DateEvenement = new DateTime(2023, 09, 06, 23, 0, 0),
                DateLimiteInscription = new DateTime(2023, 09, 05, 0, 0, 0),
                Description = "Soirée Back to School pour bien commencer l'année",
                CoverPhotoPath = "/images/evenement/backtoschool2.jpg",
                MaxParticipants = 150,
                MinParticipants = 120,
                NbReservations = 0,
                NbParticipants = 143,
                PrixBillet = 12,
                Organisateur = oOrga11,
            };
            this.Evenements.Add(
                evenement1
                );
            this.SaveChanges();
            this.Reservations.Add(new Reserver
            {
                DateReservation = DateTime.Now,
                Participant = pOrga10,
                Evenement = evenement1
            });
            this.SaveChanges();

            // Ajoute de 100 profils de participants
            string[] noms = {
    "Smith", "Johnson", "Brown", "Davis", "Wilson", "Miller", "Moore", "Taylor", "Anderson", "Jackson",
    "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Lewis", "Lee",
    "Walker", "Hall", "Allen", "Young", "Wright", "King", "Scott", "Green", "Baker", "Adams",
    "Nelson", "Hill", "Ramirez", "Campbell", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner",
    "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook",
    "Rogers", "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez", "Reyes", "Russell",
    "Diaz", "Hayes", "Myers", "Ford", "Hamilton", "Graham", "Sullivan", "Wallace", "Woods", "Cole",
    "West", "Jordan", "Owens", "Reynolds", "Fisher", "Ellis", "Harrison", "Gibson", "McDonald", "Cruz",
    "Marshall", "Ortiz", "Gonzales", "Fowler", "Fleming", "Long", "Hicks", "Robertson", "Murray", "Freeman",
    "Wells", "Webb", "Simpson", "Stevens", "Tucker", "Porter", "Hunter", "Hudson", "Bishop", "Nichols"
};
            string[] prenoms = {
    "Alice", "Bob", "Charlie", "David", "Emma", "Frank", "Grace", "Henry", "Isabel", "Jack",
    "Katherine", "Liam", "Mia", "Noah", "Olivia", "Paul", "Quinn", "Ryan", "Sophia", "Thomas",
    "Uma", "Victor", "Willow", "Xander", "Yara", "Zach", "Ava", "Benjamin", "Chloe", "Daniel",
    "Emily", "Finn", "Gemma", "Hannah", "Isaac", "Julia", "Kyle", "Lily", "Mason", "Nora",
    "Owen", "Peyton", "Quincy", "Ruby", "Samuel", "Taylor", "Victoria", "William", "Ximena", "Yasmine",
    "Zane", "Abigail", "Bradley", "Cora", "Dylan", "Ella", "Fiona", "Gavin", "Haley", "Ian",
    "Jasmine", "Kaden", "Layla", "Max", "Natalie", "Oscar", "Piper", "Quinn", "Riley", "Savannah",
    "Theo", "Uma", "Violet", "Wyatt", "Xander", "Yvonne", "Zoe", "Aiden", "Brooklyn", "Caleb", "Delilah",
    "Elijah", "Faith", "Gabriel", "Hazel", "Ivy", "Jude", "Kylie", "Liam", "Madison", "Nathan", "Olivia",
    "Peyton", "Quincy", "Rebecca", "Sofia", "Tyler", "Ulysses", "Vivian", "Wyatt", "Xander", "Yasmine", "Zachary"
};

            string[] universites = { "Paris12", "Paris-Descartes", "Paris-Sorbonne", "HEC" };

            string[] bdeNoms = { "Paris12-ECO-BDE", "Paris-Descartes-Droit-BDE", "ParisSorbonne-Lettres-BDE", "HEC-ECO-BDE" };

            for (int i = 0; i < 100; i++)
            {
                var nom = noms[i % noms.Length];
                var prenom = prenoms[i % noms.Length];
                var telephone = $"+33{new Random().Next(600000000, 699999999)}";
                var universite = universites[i % universites.Length];
                var email = $"{prenom.ToLower()[0]}.{nom.ToLower()}@{universite.ToLower()}.fr";
                var bdeNom = bdeNoms[i % bdeNoms.Length];

                var compte = new Compte
                {
                    Id = i + 21,
                    Email = email,
                    MotDePasse = CompteService.EncodeMD5("rrrrr"),
                    Profil = "Participant",
                    Prenom = prenom,
                    Nom = nom,
                    NumeroTelephone = telephone,
                    PhotoProfilePath = null 
                };
                this.Comptes.Add(compte);
                this.SaveChanges();

                var participant100 = new Participant
                {
                    Id = i + 16, 
                    Compte = compte,
                    NomBDE = bdeNom,
                    Universite = universite
                };
                this.Participants.Add(participant100);

                this.Reservations.Add(new Reserver
                {
                    DateReservation = DateTime.Now,
                    Participant = participant100,
                    Evenement = evenement1
                });

                this.SaveChanges();
            }
            //---------------
            //Evenement evenement1 = new Evenement
            //{
            //    Id = 1,
            //    Titre = "Back to School",
            //    Etat = EtatEvenement.PUBLIE,
            //    Type = TypeEvenement.SOIREE,
            //    CreeLe = new DateTime(2023, 06, 4, 10, 30, 20),
            //    DateEvenement = new DateTime(2023, 09, 06, 23, 0, 0),
            //    DateLimiteInscription = new DateTime(2023, 09, 05, 0, 0, 0),
            //    Description = "Soirée Back to School pour bien commencer l'année",
            //    CoverPhotoPath = "/images/evenement/backtoschool.jpg",
            //    MaxParticipants = 150,
            //    MinParticipants = 120,
            //    NbReservations = 0,
            //    NbParticipants = 1,
            //    PrixBillet = 12,
            //    Organisateur = oOrga11,
            //};
            Evenement evenement2 = new Evenement
            {
                Id = 2,
                Titre = "Soirée d'Intégration HEC",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.SOIREE,
                CreeLe = new DateTime(2023, 08, 10, 20, 30, 20),
                DateEvenement = new DateTime(2023, 09, 08, 23, 0, 0),
                DateLimiteInscription = new DateTime(2023, 09, 01, 0, 0, 0),
                Description = "Soirée d'intégration pour les premières années",
                CoverPhotoPath = "/images/evenement/vendredibyloftclub.jpg",
                MaxParticipants = 200,
                MinParticipants = 120,
                NbReservations = 0,
                NbParticipants = 187,
                PrixBillet = 12,
                Organisateur = oOrga12,
            };
            Evenement evenement3 = new Evenement
            {
                Id = 3,
                Titre = "Let's All Chant",
                Etat = EtatEvenement.OUVERT,
                Type = TypeEvenement.SOIREE,
                CreeLe = new DateTime(2023, 08, 29, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 5, 20, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 01, 0, 0, 0),
                Description = "Ramenez vos instruments et vos cordes vocales",
                CoverPhotoPath = "/images/evenement/karaokep12.png",
                MaxParticipants = 100,
                MinParticipants = 80,
                NbReservations = 0,
                NbParticipants = 0,
                PrixBillet = 10,
                Organisateur = orgaP12,
            };
            Evenement evenement4 = new Evenement
            {
                Id = 4,
                Titre = "Concert Indie",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.CONCERT,
                CreeLe = new DateTime(2023, 10, 15, 20, 30, 20),
                DateEvenement = new DateTime(2023, 10, 15, 22, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Soirée Chill Concert",
                CoverPhotoPath = "/images/evenement/indie.png",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 56,
                NbParticipants = 56,
                PrixBillet = 5,
                Organisateur = orgaP12,
            };

            Evenement evenement5 = new Evenement
            {
                Id = 5,
                Titre = "Soirée Jeux de Société",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.JEUXDESOCIETE,
                CreeLe = new DateTime(2023, 08, 16, 20, 30, 20),
                DateEvenement = new DateTime(2023, 09, 11, 19, 0, 0),
                DateLimiteInscription = new DateTime(2023, 09, 05, 20, 0, 0),
                Description = "Ramenez vos instruments et vos cordes vocales",
                CoverPhotoPath = "/images/evenement/soireejeux.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 68,
                NbParticipants = 68,
                PrixBillet = 5,
                Organisateur = oOrga11,
            };

            Evenement evenement6 = new Evenement
            {
                Id = 6,
                Titre = "Concours de pétanque",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.SPORT,
                CreeLe = new DateTime(2023, 09, 02, 11, 30, 20),
                DateEvenement = new DateTime(2023, 10, 22, 14, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 19, 15, 0, 0),
                Description = "Apportez vos boules. On s'occupe du pastaga",
                CoverPhotoPath = "/images/evenement/Cpétanque.jpg",
                MaxParticipants = 100,
                MinParticipants = 50,
                NbReservations = 35,
                NbParticipants = 35,
                PrixBillet = 4.5,
                Organisateur = oOrga10,
            };

            Evenement evenement7 = new Evenement
            {
                Id = 7,
                Titre = "Summer Party",
                Etat = EtatEvenement.PASSE,
                Type = TypeEvenement.SOIREE,
                CreeLe = new DateTime(2023, 07, 30, 12, 30, 20),
                DateEvenement = new DateTime(2023, 08, 26, 20, 0, 0),
                DateLimiteInscription = new DateTime(2023, 08, 20, 0, 0, 0),
                Description = "Soirée pour profiter de la fin de l'été",
                CoverPhotoPath = "/images/evenement/SUMMERPARTY.jpg",
                MaxParticipants = 250,
                MinParticipants = 200,
                NbReservations = 222,
                NbParticipants = 222,
                PrixBillet = 12,
                Organisateur = oOrga11,
            };

            Evenement evenement8 = new Evenement
            {
                Id = 8,
                Titre = "Back to the 2000",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.REPAS,
                CreeLe = new DateTime(2023, 08, 16, 15, 47, 00),
                DateEvenement = new DateTime(2023, 10, 19, 19, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Preparez vos plus belles tenues FLUO des années 2000",
                CoverPhotoPath = "/images/evenement/backto2000.png",
                MaxParticipants = 180,
                MinParticipants = 130,
                NbReservations = 98,
                NbParticipants = 98,
                PrixBillet = 15,
                Organisateur = oOrga10,
            };

            Evenement evenement9 = new Evenement
            {
                Id = 9,
                Titre = "my loft is MAGIC",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.SOIREE,
                CreeLe = new DateTime(2023, 09, 01, 12, 12, 20),
                DateEvenement = new DateTime(2023, 11, 04, 23, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 31, 0, 0, 0),
                Description = "Soirée magie avec shooters à ganger toute la soirée",
                CoverPhotoPath = "/images/evenement/magic.jpg",
                MaxParticipants = 120,
                MinParticipants = 100,
                NbReservations = 26,
                NbParticipants = 26,
                PrixBillet = 12,
                Organisateur = oOrga11,
            };

            Evenement evenement10 = new Evenement
            {
                Id = 10,
                Titre = "Chasses aux trésors",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.PLEINE_AIRE,
                CreeLe = new DateTime(2023, 08, 22, 18, 36, 40),
                DateEvenement = new DateTime(2023, 09, 05, 18, 0, 0),
                DateLimiteInscription = new DateTime(2023, 08, 30, 0, 0, 0),
                Description = "Plein de cadeaux à gagner.",
                CoverPhotoPath = "/images/evenement/pirates.jpg",
                MaxParticipants = 80,
                MinParticipants = 50,
                NbReservations = 69,
                NbParticipants = 69,
                PrixBillet = 0,
                Organisateur = oOrga12,
            };
            Evenement evenement11 = new Evenement
            {
                Id = 11,
                Titre = "Beer Pong LEA vs Droit",
                Etat = EtatEvenement.PUBLIE,
                Type = TypeEvenement.SOIREE,
                CreeLe = new DateTime(2023, 06, 4, 10, 30, 20),
                DateEvenement = new DateTime(2023, 09, 06, 23, 0, 0),
                DateLimiteInscription = new DateTime(2023, 10, 15, 0, 0, 0),
                Description = "Defend l'honneur de ta fac avec ton talent surhumain",
                CoverPhotoPath = "/images/evenement/Beer-Pong.jpg",
                MaxParticipants = 100,
                MinParticipants = 70,
                NbReservations = 91,
                NbParticipants = 91,
                PrixBillet = 10,
                Organisateur = oOrga10,
            };

            this.Evenements.AddRange(
                //evenement1,
                evenement2,
                evenement3,
                evenement4,
                evenement5,
                evenement6,
                evenement7,
                evenement8,
                evenement9,
                evenement10,
                evenement11

                );
            this.SaveChanges();

            this.LignesEvenement.AddRange(
                new LigneEvenement
                {
                    Id = 1,
                    Evenement = evenement11,
                    Type = TypeDePrestation.BAR,
                    Prestation = presta10,
                    TarifProposee = 1000
                },
                new LigneEvenement
                {
                    Id = 2,
                    Evenement = evenement1,
                    Type = TypeDePrestation.SALLE,
                    Prestation = presta13,
                    TarifProposee = 1500
                },
                new LigneEvenement
                {
                    Id = 3,
                    Evenement = evenement1,
                    Type = TypeDePrestation.TRAITEUR,
                    Prestation = presta12,
                    TarifProposee = 300
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
                    Type = TypeDePrestation.PHOTOGRAPHIE,
                    Prestation = presta12
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
                    Id = 22,
                    Evenement = evenement9,
                    Type = TypeDePrestation.LOCATION,
                    Location = TypeDeLocation.PHOTOMATON
                },
                new LigneEvenement
                {
                    Id = 23,
                    Evenement = evenement3,
                    Type = TypeDePrestation.SALLE,
                }
                );
            this.SaveChanges();

            this.Reservations.Add(new Reserver
            {
                DateReservation = DateTime.Now,
                Participant = pParticip4,
                Evenement = evenement1
            });

            this.Reservations.Add(new Reserver
            {
                DateReservation = DateTime.Now,
                Participant = pParticip5,
                Evenement = evenement2
            });

            this.Reservations.Add(new Reserver
            {
                DateReservation = DateTime.Now,
                Participant = pParticip3,
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
