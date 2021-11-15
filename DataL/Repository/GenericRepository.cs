using DataLayer.Context;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        readonly MusicContext db;

        public GenericRepository(MusicContext context)
        {
            db = context;
        }

        public TEntity Create(TEntity entity)
        {
            var ent = db.Set<TEntity>().Add(entity);
            return ent.Entity;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            db.Set<TEntity>().Remove(entity);
        }

        public TEntity Get(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }

        public void Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }
    }
}
