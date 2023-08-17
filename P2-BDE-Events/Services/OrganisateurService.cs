using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;

namespace P2_BDE_Events.Services
{
    public class OrganisateurService : Dal
    {
        public int Creer(Organisateur organisateur)
        {
            _bddContext.Organisateurs.Add(organisateur);
            _bddContext.SaveChanges();
            return organisateur.Id;
        }

        public void Modifier(int id, Organisateur modifications)
        {
            Organisateur cible = _bddContext.Organisateurs.Find(id);
            if (cible == null)
            {
                cible = modifications;
                _bddContext.SaveChanges(true);
            }
        }


    }
}
