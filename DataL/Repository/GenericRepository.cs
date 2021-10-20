using DataLayer.Context;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MusicContext db;

        public GenericRepository(MusicContext context)
        {
            this.db = context;
        }

        public T Get(int id)
        {
            return db.Find<T>(id);
        }

        public void Create(T item)
        {
            db.Add(item);
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T item = db.Find<T>(id);
            if (item != null)
                db.Remove(item);
        }
    }
}
