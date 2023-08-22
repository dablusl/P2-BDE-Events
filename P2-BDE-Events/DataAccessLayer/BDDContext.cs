using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System;

namespace P2_BDE_Events.DataAccessLayer
{
    public class BDDContext : DbContext
    {
        public DbSet<Organisateur> Organisateurs { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Prestataire> Prestataires { get; set; }
        public DbSet<Prestation>  Prestations { get; set; }
        public DbSet<FacturePrestation> FacturePrestations { get; set; }
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
                },
                new Organisateur
                {
                    Id = 2,
                    Email = "orga2@organisateur.com",
                    MotDePasse = "MDP",
                    Prenom = "Jean",
                    Nom = "Jacques",
                    NumeroTelephone = "020201020202"
                }
            );
            this.Prestations.AddRange(
                 new Prestation
                 {
                     Id = 1,
                     Titre = "Ma Prestation",
                     Type = Models.Prestations.Enums.TypeDePrestation.SALLE,
                     CapaciteMax = 10,
                     Tarif = 100,
                     Calendrier = "Du 1er au 5 août",
                     Livraison = true,
                     Description = "Une description de la prestation",
                     Etat = EtatDePrestation.EnAttenteDeValidation
                 });
                new Prestation
                {
                    Id = 2,
                    Titre = "Votre Dj",
                    Type = Models.Prestations.Enums.TypeDePrestation.DJ,
                    CapaciteMax = 50,
                    Tarif = 200,
                    Calendrier = "Tout août",
                    Livraison = true,
                    Description = "Dj pour vos soirée" ,
                    Etat = EtatDePrestation.EnCours
                });
                new Prestation
                {
                    Id = 3,
                    Titre = "Ma Prestation",
                    Type = Models.Prestations.Enums.TypeDePrestation.TRAITEUR,
                    CapaciteMax = 300,
                    Tarif = 1000,
                    Calendrier = "3eme weekend du mois",
                    Livraison = true,
                    Description = "Traiteur spécialisé en calamar",
                    Etat = EtatDePrestation.Annulee
                });
                 new FacturePrestation
                 {
                    NumeroFacture = 20230101,
                        Date = 2023 / 01 / 01,
                        MontantHT =   1000,
                        Prestation = 3
                 });
                new FacturePrestation
                {
                    NumeroFacture = 20230201,
                        Date = 01 / 02 / 2023,
                        MontantHT = 2000,
                        Prestation = 2
                });
                new FacturePrestation
                {
                    NumeroFacture = 20230506,
                            Date = 06 / 05 / 2023,
                            MontantHT = 500,
                            Prestation = 2
                });
                new FacturePrestation
                {
                    NumeroFacture = 20231011,
                            Date = 11 / 10 / 2023,
                            MontantHT = 3000,
                            Prestation = 3
                });
                new FacturePrestation
                {
                    NumeroFacture = 20230706,
                    Date = 2023 / 07 / 06,
                            MontantHT = 5000,
                            Prestation = 1
                });

            this.SaveChanges();
        }
    }
}
