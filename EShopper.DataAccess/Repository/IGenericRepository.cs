using EShopper.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EShopper.DataAccess.Repository
{
    public interface IGenericRepository<TEntity, IdType>
        where TEntity : class, IEntity, new()
    {
        TEntity GetById(IdType id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        void Update(TEntity entity);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}