using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository.Interface;
using System;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicContext db;
        public IGenericRepository<User> User { get; }
        public IGenericRepository<Song> Song { get; }
        public IGenericRepository<Playlist> Playlist { get; }
        public UnitOfWork(MusicContext _context,
                    IGenericRepository<User> user,
                    IGenericRepository<Song> song,
                    IGenericRepository<Playlist> playlist)
        {
            this.db = _context;

            this.User = user;
            this.Song = song;
            this.Playlist = playlist;
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
