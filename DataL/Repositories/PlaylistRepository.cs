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
        private readonly MusicContext db;

        public PlaylistRepository(MusicContext context)
        {
            db = context;
        }

        public void Create(Playlist playlist)
        {
            db.Playlists.Add(playlist);
        }
        
        public void Update(Playlist playlist)
        {
            db.Entry(playlist).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Playlist playlist = db.Playlists.Find(id);
            if (playlist != null)
                db.Playlists.Remove(playlist);
        }

        public IEnumerable<Playlist> Find(Func<Playlist, bool> predicate)
        {
            return db.Playlists.Include(p => p.Users).Include(p => p.Songs).Where(predicate).ToList();
        }

        public Playlist Get(Guid id)
        {
            return db.Playlists.Include(p => p.Users).Include(p => p.Songs).First(p => p.Id == id);
        }

        public IEnumerable<Playlist> GetAll()
        {
            return db.Playlists.Include(p => p.Users).Include(p => p.Songs);
        }
    }
}
