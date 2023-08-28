using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Services;
using System.Collections.Generic;
using System;
using System.Linq;

namespace P2_BDE_Events.DataAccessLayer
{
    public class Dal : IDal
    {
        protected BDDContext _bddContext;
        public Dal()
        {
            _bddContext = new BDDContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
