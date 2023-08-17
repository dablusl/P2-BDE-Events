using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Services;
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
            // Nous supprimons la base si elle existe puis nous la créons
            using (OrganisateurService organisateurService = new OrganisateurService())
            {
                // Nous supprimons et créons la db avant le test
                organisateurService.DeleteCreateDatabase();
                // Nous créons un organisateru
                Organisateur organisateur1 = new Organisateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                organisateurService.CreerOrganisateur(organisateur1);

                // Nous vérifions que le lieu a bien été créé
                List<Organisateur> utilisateur = organisateurService.ObtenirTousLesOrganisateurs();
                Assert.NotNull(utilisateur);
                Assert.Single(utilisateur);
                Assert.Equal(organisateur1.Email, utilisateur[0].Email);

                // Nous nettoyons la base
                organisateurService.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Modifier_Organisateur_Verification()
        {
            using (OrganisateurService organisateurService = new OrganisateurService())
            {
                // Nous supprimons et créons la db avant le test
                organisateurService.DeleteCreateDatabase();

                Organisateur organisateur1 = new Organisateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                organisateurService.CreerOrganisateur(organisateur1);

                organisateurService.ModifierOrganisateur(1, organisateur1 );
                // Nous vérifions que le lieu a bien été créé
                List<Organisateur> utilisateur = organisateurService.ObtenirTousLesOrganisateurs();
                Assert.NotNull(utilisateur);
                Assert.Single(utilisateur);
                Assert.Equal(organisateur1.Email, utilisateur[0].Email);

                // Nous nettoyons la base
                organisateurService.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Supprimer_Organisateur_Verification()
        {
            using (OrganisateurService organisateurService = new OrganisateurService())
            {
                // Nous supprimons et créons la db avant le test
                organisateurService.DeleteCreateDatabase();

                Organisateur organisateur1 = new Organisateur { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                organisateurService.CreerOrganisateur(organisateur1);

                organisateurService.SupprimerOrganisateur(1);
                // Nous vérifions que le lieu a bien été créé
                List<Organisateur> utilisateur = organisateurService.ObtenirTousLesOrganisateurs();
                Assert.NotNull(utilisateur);
                Assert.Empty(utilisateur);

                // Nous nettoyons la base
                organisateurService.DeleteCreateDatabase();
            }
        }
    }
}
