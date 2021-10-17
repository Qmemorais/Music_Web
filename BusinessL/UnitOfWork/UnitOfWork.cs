using BusinessL.Services;
using DataL.Context;
using System;

namespace BusinessL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private MusicContext db = new();
        private UserRepository userRepository;
        private PlaylistRepository playlistRepository;
        private SongRepository songRepository;
        public UserRepository User
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public PlaylistRepository Playlist
        {
            get
            {
                if (playlistRepository == null)
                    playlistRepository = new PlaylistRepository(db);
                return playlistRepository;
            }
        }
        public SongRepository Song
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
