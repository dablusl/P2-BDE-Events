using Microsoft.EntityFrameworkCore;

namespace P2_BDE_Events.DataAccessLayer
{
    public class Dal
    {
        private BDDContext _bddContext;
        public Dal()
        {
            _bddContext = new BDDContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }
    }
}
