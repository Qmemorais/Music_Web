using DataLayer.Context;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        MusicContext db;
        DbSet<TEntity> dbSet;

        public GenericRepository(MusicContext context)
        {
            this.db = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
