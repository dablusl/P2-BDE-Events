using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services
{
    public class OrganisateurService
    {
        private readonly BDDContext _bddContext;

        public OrganisateurService(BDDContext bddContext)
        {
            _bddContext = bddContext;
        }
        public int CreerOrganisateur(Organisateur organisateur)
        {
            _bddContext.Organisateurs.Add(organisateur);
            _bddContext.SaveChanges();
            return organisateur.Id;
        }

        public void ModifierOrganisateur(int id, Organisateur modifications)
        {
            Organisateur cible = _bddContext.Organisateurs.Find(id);
            if (cible == null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public Organisateur  ObtenirOrganisateur(int id)
        {
            return _bddContext.Organisateurs.Find(id);
        }

        public List<Organisateur> ObtenirTousLesOrganisateurs()
        {
            return _bddContext.Organisateurs.ToList();
        }

        public void SupprimerOrganisateur(int id)
        {
            Organisateur cible = _bddContext.Organisateurs.Find(id);
            if (cible == null)
            {
                _bddContext.Organisateurs.Remove(cible);
                _bddContext.SaveChanges();
            }
        }
    }
}
