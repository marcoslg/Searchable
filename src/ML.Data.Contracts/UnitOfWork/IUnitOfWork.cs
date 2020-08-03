using System;
using ML.Data.Contracts.Respositories;

namespace ML.Data.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        void Dispose(bool disposing);
        IWriteRepository<T> GetRepository<T>() where T : class;
        IQueryRepository<T> GetQueryRepository<T>() where T : class;
    }
}
