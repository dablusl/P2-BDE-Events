using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Services.Comptes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProjectBDEEvents.TestGestionComptes
{
    [Collection("Database tests")]
    public class TestgestionCompteAdministrateur
    {
        [Fact]
        public void Creation_Administrateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();

                using (AdministrateurService administrateurService = new AdministrateurService())
                {
                    // Execution
                    Administrateur administrateur = new Administrateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    administrateurService.CreerAdministrateur(administrateur);

                    // Verification
                    List<Administrateur> administrateurs = administrateurService.ObtenirTousLesAdministrateurs();
                    Assert.NotNull(administrateurs);
                    Assert.Single(administrateurs);
                    Assert.Equal(administrateur.Email, administrateurs[0].Email);

                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Modifier_Addministrateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();

                using (AdministrateurService administrateurService = new AdministrateurService())
                {
                    // Execution
                    Administrateur administrateur = new Administrateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    administrateurService.CreerAdministrateur(administrateur);

                    Administrateur nouvelAdministrateur = new Administrateur { Email = "orgaAZEAZEA2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    administrateurService.ModifierAdministrateur(1, nouvelAdministrateur);
                    // Verification
                    List<Administrateur> administrateurs = administrateurService.ObtenirTousLesAdministrateurs();
                    Assert.NotNull(administrateurs);
                    Assert.Single(administrateurs);
                    Assert.Equal(nouvelAdministrateur.Email, administrateurs[0].Email);
                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Supprimer_Administrateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                using (AdministrateurService administrateurService = new AdministrateurService())
                {
                    // Execution
                    Administrateur administrateur = new Administrateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    administrateurService.CreerAdministrateur(administrateur);
                    administrateurService.SupprimerAdministrateur(1);

                    // Verification
                    List<Administrateur> administrateurs = administrateurService.ObtenirTousLesAdministrateurs();
                    Assert.NotNull(administrateur);
                    Assert.Empty(administrateurs);
                }
                dal.DeleteCreateDatabase();
            }
        }
    }
}
