using ML.DataStructure.Collections;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ML.Data.Contracts.Respositories
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool trackingEnabled = false);

        TEntity FindById(object id);        
        Task<TEntity> FindByIdAsync(object[] id, CancellationToken cancellationToken = default);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackingEnabled = false);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, bool trackingEnabled = false);

        PagedList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, int page = 0, int pageSize = 50, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool trackingEnabled = false);
        PagedList<TEntity> Get(IQueryable<TEntity> query, int page = 0, int pageSize = 50, bool trackingEnabled = false);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool trackingEnabled = false);

        IQueryable<TEntity> GetAll(bool trackingEnabled = false);

    }
}
