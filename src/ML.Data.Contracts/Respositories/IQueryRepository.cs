using ML.DataStructure.Collections;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ML.Data.Contracts.Respositories
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity FindById(object id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackingEnabled = false);

        PagedList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, int page = 0, int pageSize = 50, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool trackingEnabled = false);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool trackingEnabled = false);

        IQueryable<TEntity> GetAll();

    }
}
