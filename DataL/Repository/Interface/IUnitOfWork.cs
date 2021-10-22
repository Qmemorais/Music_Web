using DataLayer.Models;
using System;

namespace DataLayer.Repository.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<User> User { get; }
        IGenericRepository<Song> Song { get; }
        IGenericRepository<Playlist> Playlist { get; }
        void Save();
    }
}
