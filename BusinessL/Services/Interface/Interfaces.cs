using System;
using System.Collections.Generic;

namespace BusinessL.Services.Interface
{
    interface IRepository<T> where T : class
    {
        T Get(int id);
        void Create(T item);
        void Delete(int id);
    }
    interface IUserRepository<User>
    {
        void Update(User item);
    }
    interface IPlaylistRepository<Playlist>
    {
        IEnumerable<Playlist> GetAll(int id);
        void Update(Playlist item);
    }
    interface ISongRepository<Song>
    {
        IEnumerable<Song> GetAll(int id);
    }
}
