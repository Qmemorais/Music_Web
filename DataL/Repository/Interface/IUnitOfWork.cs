using System;

namespace DataLayer.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<object> Generic { get; }
        IGetListRepository<object> GetList { get; }
        void Save();
    }
}
