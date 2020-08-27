using System;
using System.Threading;
using System.Threading.Tasks;
using ML.Data.Contracts.Respositories;

namespace ML.Data.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        void Dispose(bool disposing);
        IWriteRepository<T> GetRepository<T>() where T : class;
        IQueryRepository<T> GetQueryRepository<T>() where T : class;
    }
}
