using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Evenements
{
    public class LitigeService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public LitigeService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerLitige(Litige litige)
        {
            _bddContext.Litiges.Add(litige);
            _bddContext.SaveChanges();
            return litige.Id;
        }

        public void ModifierLitige(int id, Litige modifications)
        {
            Litige cible = _bddContext.Litiges.Find(id);
            if (cible != null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public Litige ObtenirLitige(int id)
        {
            return _bddContext.Litiges.Find(id);
        }

        public List<Litige> ObtenirTousLesLitiges()
        {
            return _bddContext.Litiges.ToList();
        }

        public void SupprimerLitige(int id)
        {
            Litige cible = _bddContext.Litiges.Find(id);
            if (cible != null)
            {
                _bddContext.Litiges.Remove(cible);
                _bddContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
