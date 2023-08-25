using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Models.Prestations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2_BDE_Events
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        //static void Main(string[] args)
        //{
        //    // Exemple d'utilisation
        //    Prestation prestation = new Prestation
        //    {
        //        Titre = "Ma Prestation",
        //        Type = TypeDePrestation.Type1,
        //        CapaciteMax = 10,
        //        Tarif = 100,
        //        Calendrier = "Du 1er au 5 août",
        //        Livraison = true,
        //        Description = "Une description de la prestation",
        //        Etat = EtatDePrestation.EnCours
        //    };

        //    Prestataire prestataire = new Prestataire
        //    {
        //        Adresse = "123 Rue de Prestataire",
        //        ZoneDeDeplacement = "Zone A",
        //        Horaires = "9h00 - 18h00",
        //        Bio = "Biographie du prestataire",
        //        Siret = "123456789",
        //        RaisonSociale = "Ma Société"
        //    };

        //    FacturePrestation facture = new FacturePrestation
        //    {
        //        NumeroFacture = 1,
        //        Date = DateTime.Now,
        //        MontantHT = 1000
        //    };

        //    prestation.Factures.Add(facture);

        //    LigneEvenement evenement = new LigneEvenement
        //    {
        //        Prestataire = prestataire,
        //        Prestation = prestation,
        //        EtatLitige = EtatLitigeEvenement.EnCours
        //    };
        //}
    }
}
