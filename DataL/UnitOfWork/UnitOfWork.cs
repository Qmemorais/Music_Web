using DataLayer.Context;
using DataLayer.Repository.Interface;
using System;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicContext db;
        public IGenericRepository<object> Generic { get; }
        public IUserRepository<object> User { get; }
        public UnitOfWork(MusicContext _context,
                    IGenericRepository<object> generic,
                    IUserRepository<object> user)
        {
            this.db = _context;

            this.Generic = generic;
            this.User = user;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
