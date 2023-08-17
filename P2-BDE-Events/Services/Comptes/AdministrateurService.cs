using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Comptes
{
    public class AdministrateurService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public AdministrateurService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerAdministrateur(Administrateur administrateur)
        {
            _bddContext.Organisateurs.Add(administrateur);
            _bddContext.SaveChanges();
            return administrateur.Id;
        }

        public void ModifierAdministrateur(int id, Administrateur modifications)
        {
            Administrateur cible = _bddContext.Administrateurs.Find(id);
            if (cible != null)
            {
                cible.Email = modifications.Email;
                cible.Prenom = modifications.Prenom;
                cible.Nom = modifications.Nom;
                cible.NumeroTelephone = modifications.NumeroTelephone;

                _bddContext.SaveChanges();
            }
        }

        public Administrateur ObtenirAdministrateur(int id)
        {
            return _bddContext.Administrateurs.Find(id);
        }

        public List<Administrateur> ObtenirTousLesAdministrateurs()
        {
            return _bddContext.Administrateurs.ToList();
        }

        public void SupprimerAdministrateur(int id)
        {
            Administrateur cible = _bddContext.Administrateurs.Find(id);
            if (cible != null)
            {
                _bddContext.Administrateurs.Remove(cible);
                _bddContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
