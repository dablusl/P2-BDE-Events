using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Evenements
{
    public class CommentaireEvenementService
    {
        private readonly BDDContext _bddContext;

        public CommentaireEvenementService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerCommentaireEvenement(CommentaireEvenement commentaire)
        {
            _bddContext.CommentaireEvenements.Add(commentaire);
            _bddContext.SaveChanges();

            return commentaire.Id;
        }

        public void ModifierCommentaireEvenement(int id, CommentaireEvenement modifications)
        {
            CommentaireEvenement cible = _bddContext.CommentaireEvenements.Find(id);
            if (cible != null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public CommentaireEvenement ObtenirCommentaireEvenement(int id)
        {
            return _bddContext.CommentaireEvenements.Find(id);
        }

        public void SupprimerCommentaireEvenement(int id)
        {
            CommentaireEvenement cible = _bddContext.CommentaireEvenements.Find(id);
            if (cible != null)
            {
                _bddContext.CommentaireEvenements.Remove(cible);
                _bddContext.SaveChanges();
            }
        }
    }
}
