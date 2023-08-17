using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Models.Compte;
using System.ComponentModel.DataAnnotations;

namespace TestProjectBDEEvents.TestGestionComptes
{
    [Collection("Database tests")]
    public class TestGestionComptesOrganisateur
    {
        [Fact]
        public void Creation_Organisateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();

                using (OrganisateurService organisateurService = new OrganisateurService())
                {
                    // Execution
                    Organisateur organisateur1 = new Organisateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    organisateurService.CreerOrganisateur(organisateur1);

                    // Verification
                    List<Organisateur> utilisateur = organisateurService.ObtenirTousLesOrganisateurs();
                    Assert.NotNull(utilisateur);
                    Assert.Single(utilisateur);
                    Assert.Equal(organisateur1.Email, utilisateur[0].Email);

                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Modifier_Organisateur_Verification()
        {
            using (Dal dal = new Dal())
            {
                using (OrganisateurService organisateurService = new OrganisateurService())
                {
                    // Execution
                    Organisateur organisateur1 = new Organisateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    organisateurService.CreerOrganisateur(organisateur1);

                    organisateurService.ModifierOrganisateur(1, organisateur1);
                    // Verification
                    List<Organisateur> utilisateur = organisateurService.ObtenirTousLesOrganisateurs();
                    Assert.NotNull(utilisateur);
                    Assert.Single(utilisateur);
                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Supprimer_Organisateur_Verification()
        {
            using(Dal dal = new Dal()) {
                dal.DeleteCreateDatabase();
                using (OrganisateurService organisateurService = new OrganisateurService())
            {
                // Execution
                Organisateur organisateur1 = new Organisateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                organisateurService.CreerOrganisateur(organisateur1);
                organisateurService.SupprimerOrganisateur(1);

                // Verification
                List<Organisateur> utilisateur = organisateurService.ObtenirTousLesOrganisateurs();
                Assert.NotNull(utilisateur);
                Assert.Empty(utilisateur);
            }
                dal.DeleteCreateDatabase();
            }
        }
    }
}
