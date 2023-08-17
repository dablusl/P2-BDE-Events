using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Evenements
{
    public class PhotoService
    {
        private readonly BDDContext _bddContext;

        public PhotoService()
        {
            _bddContext = new BDDContext();
        }
        public int AjouterPhoto(Photo photo)
        {
            _bddContext.Photos.Add(photo);
            _bddContext.SaveChanges();
            return photo.Id;
        }

        public Photo ObtenirPhoto(int id)
        {
            return _bddContext.Photos.Find(id);
        }

        public List<Photo> ObtenirTousLesPhotos()
        {
            return _bddContext.Photos.ToList();
        }

        public void SupprimerPhoto(int id)
        {
            Photo cible = _bddContext.Photos.Find(id);
            if (cible != null)
            {
                _bddContext.Photos.Remove(cible);
                _bddContext.SaveChanges();
            }
        }
    }
}
