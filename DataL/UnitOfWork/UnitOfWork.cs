using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicContext _db;
        public IGenericRepository<User> User;
        public IGenericRepository<Song> Song;
        public IGenericRepository<Playlist> Playlist;
        public IGenericRepository<Album> Album;
        public IGenericRepository<Artist> Artist;
        public IGenericRepository<UserPlaylist> UserPlaylist;
        public IGenericRepository<PlaylistSong> PlaylistSong;

        public UnitOfWork(MusicContext context)
        {
            _db = context;
        }

        public IGenericRepository<User> Users => User ??= new GenericRepository<User>(_db);
        public IGenericRepository<Song> Songs => Song ??= new GenericRepository<Song>(_db);
        public IGenericRepository<Playlist> Playlists => Playlist ??= new GenericRepository<Playlist>(_db);
        public IGenericRepository<Album> Albums => Album ??= new GenericRepository<Album>(_db);
        public IGenericRepository<Artist> Artists => Artist ??= new GenericRepository<Artist>(_db);
        public IGenericRepository<UserPlaylist> UserPlaylists => UserPlaylist ??= new GenericRepository<UserPlaylist>(_db);
        public IGenericRepository<PlaylistSong> PlaylistSongs => PlaylistSong ??= new GenericRepository<PlaylistSong>(_db);

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
