using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using CRM.Model.Interfaces;

namespace CRM.Inf.RepositoryFiles
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            if (context == null)
                throw new NullReferenceException();
            Context = context;
        }

        public TEntity Get(int id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().RemoveRange(entities);
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }
    }
}
