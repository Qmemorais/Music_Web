using System.Collections.Generic;

namespace DataLayer.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        public T Get(int id);
        public void Create(T item);
        public void Update(T item);
        public void Delete(int id);
    }
}
