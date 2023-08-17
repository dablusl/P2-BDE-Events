using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Services.Comptes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProjectBDEEvents.TestGestionComptes
{
    public class TestGestionComptesPrestataire
    {
        [Fact]
        public void Creation_Prestataire_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();

                using (PrestataireService prestataireService = new PrestataireService())
                {
                    // Execution
                    Prestataire prestataire = new Prestataire { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    prestataireService.CreerPrestataire(prestataire);

                    // Verification
                    List<Prestataire> prestataires = prestataireService.ObtenirTousLesPrestataires();
                    Assert.NotNull(prestataires);
                    Assert.Single(prestataires);
                    Assert.Equal(prestataire.Email, prestataires[0].Email);

                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Modifier_Prestataire_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();

                using (PrestataireService prestataireService = new PrestataireService())
                {
                    // Execution
                    Prestataire prestataire = new Prestataire { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    prestataireService.CreerPrestataire(prestataire);

                    Prestataire nouveauPrestataire = new Prestataire { Email = "orgaAZEAZEA2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    prestataireService.ModifierPrestataire(1, nouveauPrestataire);
                    // Verification
                    List<Prestataire> prestataires = prestataireService.ObtenirTousLesPrestataires();
                    Assert.NotNull(prestataires);
                    Assert.Single(prestataires);
                    Assert.Equal(nouveauPrestataire.Email, prestataires[0].Email);
                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Supprimer_Prestataire_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                using (PrestataireService prestataireService = new PrestataireService())
                {
                    // Execution
                    Prestataire prestataire = new Prestataire { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    prestataireService.CreerPrestataire(prestataire);
                    prestataireService.SupprimerPrestataire(1);

                    // Verification
                    List<Prestataire> prestataires = prestataireService.ObtenirTousLesPrestataires();
                    Assert.NotNull(prestataires);
                    Assert.Empty(prestataires);
                }
                dal.DeleteCreateDatabase();
            }
        }
    }
}
