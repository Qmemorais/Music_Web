using DataLayer.Models;
using System;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Playlist> Playlists { get; }
        IRepository<Song> Songs { get; }
        IRepository<Album> Albums { get; }
        IRepository<Artist> Artists { get; }
        void Save();
    }
}
