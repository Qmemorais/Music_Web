using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly MusicContext db;

        public UserRepository(MusicContext context)
        {
            db = context;
        }
        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Include(u => u.Playlists).Where(predicate).ToList();
        }

        public User Get(Guid id)
        {
            return db.Users.Include(u => u.Playlists).First(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(u => u.Playlists);
        }
    }
}
