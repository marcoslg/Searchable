using Microsoft.EntityFrameworkCore;
using ML.Data.Contracts.Respositories;
using System.Collections.Generic;

namespace ML.Infrastructure.Persistence.EF.Repositories
{
    public class WriteRepository<TEntity> : QueryRepository<TEntity>, IWriteRepository<TEntity> where TEntity : class
    {  
        public WriteRepository(DbContext context) 
            : base(context)
        {

        }

        public void Insert(TEntity entity)
        {
            _DbSet.Add(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            bool detectChangesEnabled = Context.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                if (detectChangesEnabled)
                    Context.ChangeTracker.AutoDetectChangesEnabled = false;
                _DbSet.AddRange(entities);
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }


        public void Update(TEntity entity)
        {
            if (entity == null || Context.Entry(entity).State != EntityState.Detached)
                return;
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            bool detectChangesEnabled = Context.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                if (detectChangesEnabled)
                    Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                    Update(entity);
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }



        public void Delete(object id)
        {
            TEntity entity = _DbSet.Find(id);
            if (entity == null)
                return;
            Delete(entity);
        }

        public void DeleteMany(IEnumerable<object> ids)
        {
            try
            {
                List<TEntity> list = new List<TEntity>();
                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (object obj in ids)
                {
                    TEntity entity = _DbSet.Find(obj);
                    if (entity != null)
                        list.Add(entity);
                }
                DeleteMany(list);
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                return;
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void DeleteMany(IEnumerable<TEntity> entities)
        {
            bool detectChangesEnabled = Context.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                if (entities == null)
                    return;
                if (detectChangesEnabled)
                    Context.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                    Delete(entity);
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }
    }
}
