using System.Collections.Generic;
using System.Data.Entity;

namespace DB.Infrastructure
{
    public interface IRepository<TEntity>
    {
        DbContext Context { get; }
        IEnumerable<TEntity> All { get; }
        TEntity GetById(int id);
        TEntity Create();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void DeleteById(int id);
    }
}
