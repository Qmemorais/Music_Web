using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    class SongRepository : IRepository<Song>
    {
        private readonly MusicContext db;

        public SongRepository(MusicContext context)
        {
            db = context;
        }

        public void Create(Song song)
        {
            db.Songs.Add(song);
        }

        public void Update(Song song)
        {
            db.Entry(song).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Song song = db.Songs.Find(id);
            if (song != null)
                db.Songs.Remove(song);
        }

        public IEnumerable<Song> Find(Func<Song, bool> predicate)
        {
            return db.Songs.Include(s => s.Playlists).Where(predicate).ToList();
        }

        public Song Get(Guid id)
        {
            return db.Songs.Include(s => s.Playlists).First(s => s.Id == id);
        }

        public IEnumerable<Song> GetAll()
        {
            return db.Songs.Include(s => s.Playlists);
        }
    }
}
