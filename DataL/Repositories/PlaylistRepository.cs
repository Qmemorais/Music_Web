using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    class PlaylistRepository : IRepository<Playlist>
    {
        private MusicContext db;

        public PlaylistRepository(MusicContext context)
        {
            this.db = context;
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

        public IEnumerable<Playlist> Find(Func<Playlist, bool> predicate)
        {
            return db.Playlists.Include(p => p.Users).Include(p => p.Songs).Where(predicate).ToList();
        }

        public Playlist Get(int id)
        {
            return db.Playlists.Find(id);
        }

        public IEnumerable<Playlist> GetAll()
        {
            return db.Playlists.Include(p => p.Users).Include(p => p.Songs);
        }
    }
}
