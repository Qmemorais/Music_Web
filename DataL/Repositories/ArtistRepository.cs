using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    class ArtistRepository : IRepository<Artist>
    {
        private MusicContext db;

        public ArtistRepository(MusicContext context)
        {
            this.db = context;
        }

        public void Create(Artist artist)
        {
            db.Artists.Add(artist);
        }

        public void Update(Artist artist)
        {
            db.Entry(artist).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist != null)
                db.Artists.Remove(artist);
        }

        public IEnumerable<Artist> Find(Func<Artist, bool> predicate)
        {
            return db.Artists.Include(a => a.Songs).Include(a => a.Albums).Where(predicate).ToList();
        }

        public Artist Get(int id)
        {
            return db.Artists.Find(id);
        }

        public IEnumerable<Artist> GetAll()
        {
            return db.Artists.Include(a => a.Songs).Include(a => a.Albums);
        }
    }
}
