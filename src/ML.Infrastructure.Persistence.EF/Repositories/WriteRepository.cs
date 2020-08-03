using System.Collections.Generic;
using System.Data.Entity;
using Common.Data.Contracts.Respositories;
using ML.Infrastructure.Persistence.EF.Configuration;
using ML.Infrastructure.Persistence.EF.Repositories;

namespace Common.Infrastructure.EF.Repositories
{
    public class WriteRepository<TEntity> : QueryRepository<TEntity>, IWriteRepository<TEntity> where TEntity : class
    {

        public WriteRepository(EFCommonConfiguration configuration) : base(configuration)
        {

        }
        public WriteRepository(DbContext context) : base(context)
        {

        }

        public void Insert(TEntity entity)
        {
            this._DbSet.Add(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            bool detectChangesEnabled = this.Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                if (detectChangesEnabled)
                    this.Context.Configuration.AutoDetectChangesEnabled = false;
                this._DbSet.AddRange(entities);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }


        public void Update(TEntity entity)
        {
            if ((object)entity == null || this.Context.Entry<TEntity>(entity).State != EntityState.Detached)
                return;
            this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void UpdateMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            bool detectChangesEnabled = this.Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                if (detectChangesEnabled)
                    this.Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                    this.Update(entity);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }



        public void Delete(object id)
        {
            TEntity entity = this._DbSet.Find(id);
            if ((object)entity == null)
                return;
            this.Delete(entity);
        }

        public void DeleteMany(IEnumerable<object> ids)
        {
            try
            {
                List<TEntity> list = new List<TEntity>();
                this.Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (object obj in ids)
                {
                    TEntity entity = this._DbSet.Find(obj);
                    if ((object)entity != null)
                        list.Add(entity);
                }
                this.DeleteMany((IEnumerable<TEntity>)list);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void Delete(TEntity entity)
        {
            if ((object)entity == null)
                return;
            this.Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public virtual void DeleteMany(IEnumerable<TEntity> entities)
        {
            bool detectChangesEnabled = this.Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                if (entities == null)
                    return;
                if (detectChangesEnabled)
                    this.Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                    this.Delete(entity);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }
    }
}
