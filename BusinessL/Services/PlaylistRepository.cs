using BusinessL.Services.Interface;
using DataL.Context;
using DataL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusinessL.Services
{
    public class PlaylistRepository : IRepository<Playlist>,IPlaylistRepository<Playlist>
    {
        private MusicContext db;

        public PlaylistRepository(MusicContext context)
        {
            this.db = context;
        }
        public IEnumerable<Playlist> GetAll(int id)
        {
            return db.Playlists.Where(playlist => playlist.UserId == id);
        }

        public Playlist Get(int id)
        {
            return db.Playlists.Find(id);
        }

        public void Create(Playlist playlist)
        {
            db.Playlists.Add(playlist);
        }

        public void Update(Playlist playlist)
        {
            db.Entry(playlist).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            if (playlist != null)
                db.Playlists.Remove(playlist);
        }
    }
}
