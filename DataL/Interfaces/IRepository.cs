using System;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(Guid id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(Guid id);
    }
}
