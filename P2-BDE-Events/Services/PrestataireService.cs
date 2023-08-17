using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services
{
    public class PrestataireService
    {
        private readonly BDDContext _bddContext;

        public PrestataireService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerPresetataire(Prestataire prestataire)
        {
            _bddContext.Prestataires.Add(prestataire);
            _bddContext.SaveChanges();
            return prestataire.Id;
        }

        public void ModifierPrestataire(int id, Prestataire modifications)
        {
            Prestataire cible = _bddContext.Prestataires.Find(id);
            if (cible == null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public Prestataire ObtenirPrestataire(int id)
        {
            return _bddContext.Prestataires.Find(id);
        }

        public List<Prestataire> ObtenirTousLesPrestataires()
        {
            return _bddContext.Prestataires.ToList();
        }

        public void SupprimerPrestataire(int id)
        {
            Prestataire cible = _bddContext.Prestataires.Find(id);
            if (cible == null)
            {
                _bddContext.Prestataires.Remove(cible);
                _bddContext.SaveChanges();
            }
        }
    }
}
