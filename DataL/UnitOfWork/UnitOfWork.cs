using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using System;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicContext _db;
        public IGenericRepository<User> User;
        public IGenericRepository<Song> Song;
        public IGenericRepository<Playlist> Playlist;
        public UnitOfWork(MusicContext context)
        {
            _db = context;
        }

        public IGenericRepository<User> Users => User ??= new GenericRepository<User>(_db);
        public IGenericRepository<Song> Songs => Song ??= new GenericRepository<Song>(_db);
        public IGenericRepository<Playlist> Playlists => Playlist ??= new GenericRepository<Playlist>(_db);

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
