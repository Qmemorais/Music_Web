using DataLayer.Models;
using DataLayer.Repository;
using System;

namespace DataLayer.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Song> Songs { get; }
        IGenericRepository<Playlist> Playlists{ get; }
        IGenericRepository<Album> Albums { get;}
        IGenericRepository<Artist> Artists { get;}
        void Save();
    }
}
