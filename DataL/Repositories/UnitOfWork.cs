using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly MusicContext _db;
        private UserRepository User;
        private SongRepository Song;
        private PlaylistRepository Playlist;
        private AlbumRepository Album;
        private ArtistRepository Artist;

        public UnitOfWork(MusicContext context)
        {
            _db = context;
        }

        public IRepository<User> Users => User ??= new UserRepository(_db);
        public IRepository<Song> Songs => Song ??= new SongRepository(_db);
        public IRepository<Playlist> Playlists => Playlist ??= new PlaylistRepository(_db);
        public IRepository<Album> Albums => Album ??= new AlbumRepository(_db);
        public IRepository<Artist> Artists => Artist ??= new ArtistRepository(_db);

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
