using System.Collections.Generic;
using System;

namespace P2_BDE_Events.DataAccessLayer
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();
    }
}
