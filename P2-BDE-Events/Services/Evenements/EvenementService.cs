using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Evenements
{
    public class EvenementService
    {
        private readonly BDDContext _bddContext;

        public EvenementService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerEvenement(Evenement evenement)
        {
            _bddContext.Evenements.Add(evenement);
            _bddContext.SaveChanges();
            return evenement.Id;
        }

        public void ModifierEvenement(int id, Evenement modifications)
        {
            Evenement cible = _bddContext.Evenements.Find(id);
            if (cible == null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public Evenement ObtenirEvenement(int id)
        {
            return _bddContext.Evenements.Find(id);
        }

        public List<Evenement> ObtenirTousLesEvenements()
        {
            return _bddContext.Evenements.ToList();
        }

        public void SupprimerEvenement(int id)
        {
            Evenement cible = _bddContext.Evenements.Find(id);
            if (cible == null)
            {
                _bddContext.Evenements.Remove(cible);
                _bddContext.SaveChanges();
            }
        }
    }
}
