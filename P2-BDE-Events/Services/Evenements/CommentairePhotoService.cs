using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using System;
using System.Collections.Generic;

namespace P2_BDE_Events.Services.Evenements
{
    public class CommentairePhotoService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public CommentairePhotoService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerCommentairePhoto(CommentairePhoto commentaire)
        {
            _bddContext.CommentairePhotos.Add(commentaire);
            _bddContext.SaveChanges();

            return commentaire.Id;
        }

        public void ModifierCommentairePhoto(int id, CommentairePhoto modifications)
        {
            CommentairePhoto cible = _bddContext.CommentairePhotos.Find(id);
            if (cible != null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public CommentairePhoto ObtenirCommentairePhoto(int id)
        {
            return _bddContext.CommentairePhotos.Find(id);
        }

        public void SupprimerCommentairePhoto(int id)
        {
            CommentairePhoto cible = _bddContext.CommentairePhotos.Find(id);
            if (cible != null)
            {
                _bddContext.CommentairePhotos.Remove(cible);
                _bddContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
