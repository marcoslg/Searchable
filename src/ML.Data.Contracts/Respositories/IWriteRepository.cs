using System.Collections.Generic;

namespace ML.Data.Contracts.Respositories
{
    public interface IWriteRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {

        void Delete(object id);

        void Delete(TEntity entity);

        void DeleteMany(IEnumerable<object> ids);

        void DeleteMany(IEnumerable<TEntity> entities);

        void Insert(TEntity entity);

        void InsertMany(IEnumerable<TEntity> entities);


        void Update(TEntity entity);

        void UpdateMany(IEnumerable<TEntity> entities);
    }
}
