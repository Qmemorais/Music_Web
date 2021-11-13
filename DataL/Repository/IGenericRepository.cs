﻿using System.Collections.Generic;

namespace DataLayer.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        public TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
