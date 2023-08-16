using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services
{
    public class CompteService : Dal
    {
        //private BDDContext _bddContext;
        //public CompteService()
        //{
        //    _bddContext = new BDDContext();
        //}
        public List<Organisateur> ObtenirTousLesOrganisateurs()
        {
            return _bddContext.Organisateurs.ToList();
        }


    }
}
