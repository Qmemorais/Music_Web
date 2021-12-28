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
        private readonly MusicContext db;

        public ArtistRepository(MusicContext context)
        {
            db = context;
        }

        public void Create(Artist artist)
        {
            db.Artists.Add(artist);
        }

        public void Update(Artist artist)
        {
            db.Entry(artist).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist != null)
                db.Artists.Remove(artist);
        }

        public IEnumerable<Artist> Find(Func<Artist, bool> predicate)
        {
            return db.Artists.Include(a => a.Songs).Include(a => a.Albums).Where(predicate).ToList();
        }

        public Artist Get(Guid id)
        {
            return db.Artists.Include(a => a.Songs).Include(a => a.Albums).First(a => a.Id == id);
        }

        public IEnumerable<Artist> GetAll()
        {
            return db.Artists.Include(a => a.Songs).Include(a => a.Albums);
        }
    }
}
