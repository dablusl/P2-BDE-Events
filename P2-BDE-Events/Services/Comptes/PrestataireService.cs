using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Comptes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Comptes
{
    public class PrestataireService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public PrestataireService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerPrestataire(Prestataire prestataire, int IdCompte)
        {
            Compte compte = _bddContext.Comptes.Find(IdCompte);
            prestataire.Compte = compte;
            _bddContext.Prestataires.Add(prestataire);
            _bddContext.SaveChanges();
            return prestataire.Id;
        }

        //public void ModifierPrestataire(int id, Prestataire modifications)
        //{
        //    Prestataire cible = _bddContext.Prestataires.Find(id);
        //    if (cible != null)
        //    {
        //        cible.Email = modifications.Email;
        //        cible.Prenom = modifications.Prenom;
        //        cible.Nom = modifications.Nom;
        //        cible.NumeroTelephone = modifications.NumeroTelephone;

        //        _bddContext.SaveChanges();
        //    }
        //}

        //public Prestataire ObtenirPrestataire(int id)
        //{
        //    return _bddContext.Prestataires.Find(id);
        //}

        //public List<Prestataire> ObtenirTousLesPrestataires()
        //{
        //    return _bddContext.Prestataires.ToList();
        //}

        //public void SupprimerPrestataire(int id)
        //{
        //    Prestataire cible = _bddContext.Prestataires.Find(id);
        //    if (cible != null)
        //    {
        //        _bddContext.Prestataires.Remove(cible);
        //        _bddContext.SaveChanges();
        //    }
        //}

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
