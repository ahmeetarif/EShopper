using EShopper.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EShopper.DataAccess.Repository
{
    public class GenericRepository<TEntity, IdType> : IGenericRepository<TEntity, IdType>
        where TEntity : class, IEntity, new()
    {
        protected readonly EShopperDbContext _context;
        private readonly DbSet<TEntity> _table = null;
        public GenericRepository(EShopperDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _table.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _table.Where(expression);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _table.ToList();
        }

        public TEntity GetById(IdType id)
        {
            TEntity entityData = _table.Find(id);
            return entityData;
        }

        public void Remove(TEntity entity)
        {
            TEntity entityData = _table.Find(entity);
            _table.Remove(entityData);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            IEnumerable<TEntity> entityData = _table.Where(x => x == entities);
            _table.RemoveRange(entityData);
        }
    }
}