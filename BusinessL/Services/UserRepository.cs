using BusinessL.Services.Interface;
using DataL.Context;
using DataL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessL.Services
{
    public class UserRepository : IRepository<User>, IUserRepository<User>
    {
        private MusicContext db;

        public UserRepository(MusicContext context)
        {
            this.db = context;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public User Find(string login, string passwd)
        {
            return db.Users.First(x => x.Login==login && x.Password==passwd);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
