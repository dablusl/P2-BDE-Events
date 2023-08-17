using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Evenements
{
    public class AlbumService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public AlbumService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerAlbum(Album album)
        {
            _bddContext.Albums.Add(album);
            _bddContext.SaveChanges();

            return album.Id;
        }

        public void ModifierAlbum(int id, Album modifications)
        {
            Album cible = _bddContext.Albums.Find(id);
            if (cible != null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public Album ObtenirAlbum(int id)
        {
            return _bddContext.Albums.Find(id);
        }

        public List<Album> ObtenirTousLesAlbums()
        {
            return _bddContext.Albums.ToList();
        }

        public void SupprimerAlbum(int id)
        {
            Album cible = _bddContext.Albums.Find(id);
            if (cible != null)
            {
                _bddContext.Albums.Remove(cible);
                _bddContext.SaveChanges();
            }
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
