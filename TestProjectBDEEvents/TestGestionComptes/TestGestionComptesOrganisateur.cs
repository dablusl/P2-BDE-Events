using System;
using System.Collections.Generic;
using Xunit;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.Models.Compte;

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
                dal.DeleteCreateDatabase();

                using (OrganisateurService organisateurService = new OrganisateurService())
                {
                    // Execution
                    Organisateur organisateur1 = new Organisateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    organisateurService.CreerOrganisateur(organisateur1);

                    Organisateur nouveauOrganisateur = new Organisateur { Email = "orgaAZEAZEA2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    organisateurService.ModifierOrganisateur(1, nouveauOrganisateur);
                    // Verification
                    List<Organisateur> utilisateur = organisateurService.ObtenirTousLesOrganisateurs();
                    Assert.NotNull(utilisateur);
                    Assert.Single(utilisateur);
                    Assert.Equal(nouveauOrganisateur.Email, utilisateur[0].Email);
                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Supprimer_Organisateur_Verification()
        {
            using (Dal dal = new Dal())
            {
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
