using Microsoft.EntityFrameworkCore;
using ML.Data.Contracts.Respositories;
using ML.DataStructure.Collections;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ML.Infrastructure.Persistence.EF.Repositories
{
    public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        protected DbContext Context;
        protected DbSet<TEntity> _DbSet;
               
        public QueryRepository(DbContext context)
        {
            Context = context;
            _DbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return ((IQueryable<TEntity>)_DbSet).Where(predicate);
        }

        public TEntity FindById(object id)
        {
            return _DbSet.Find(id);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackingEnabled = false)
        {
            return (trackingEnabled ? (IQueryable<TEntity>)_DbSet : (IQueryable<TEntity>)_DbSet.AsNoTracking()).FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return (IQueryable<TEntity>)_DbSet;
        }

        public virtual PagedList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            int page = 0, int pageSize = 50,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool trackingEnabled = false)
        {
            IQueryable<TEntity> query = (IQueryable<TEntity>)_DbSet;
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);

            return Get(query, page, pageSize, trackingEnabled);
        }

        public virtual PagedList<TEntity> Get(IQueryable<TEntity> query,
            int page = 0, int pageSize = 50,            
            bool trackingEnabled = false)
        {
            return new PagedList<TEntity>(trackingEnabled ? query : query.AsNoTracking(), page, pageSize);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,            
            bool trackingEnabled = false)
        {
            IQueryable<TEntity> query = (IQueryable<TEntity>)_DbSet;
            
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);
            return trackingEnabled ? query : query.AsNoTracking();
        }

    }
}
