using System.Collections.Generic;

namespace DataLayer.Repository.Interface
{
    public interface IGetListRepository<T> where T : class
    {
        public IEnumerable<T> GetAll(int id);
    }
}
