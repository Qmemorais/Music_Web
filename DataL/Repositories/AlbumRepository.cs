﻿using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    class AlbumRepository : IRepository<Album>
    {
        private MusicContext db;

        public AlbumRepository(MusicContext context)
        {
            this.db = context;
        }

        public void Create(Album album)
        {
            db.Albums.Add(album);
        }

        public void Update(Album album)
        {
            db.Entry(album).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Album album = db.Albums.Find(id);
            if (album != null)
                db.Albums.Remove(album);
        }

        public IEnumerable<Album> Find(Func<Album, bool> predicate)
        {
            return db.Albums.Include(a => a.Songs).Where(predicate).ToList();
        }

        public Album Get(int id)
        {
            return db.Albums.Find(id);
        }

        public IEnumerable<Album> GetAll()
        {
            return db.Albums.Include(a => a.Songs);
        }
    }
}
