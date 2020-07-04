using Iemedebe.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }

        public void Add(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: Could not add entity or a component of it to DB");
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await Context.Set<T>().ToListAsync().ConfigureAwait(false);
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: could not get Table's elements");
            }
        }

        public async Task<List<T>> GetAllByConditionAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await Context.Set<T>().Where(expression).ToListAsync().ConfigureAwait(false);
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: could not retrieve Entity");
            }
        }

        public abstract Task<T> GetAsync(Guid id);

        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await Context.Set<T>().FirstOrDefaultAsync(expression).ConfigureAwait(false);
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: could not retrieve Entity");
            }
        }

        public void Remove(T entity)
        {
            try
            {
                Context.Set<T>().Remove(entity);
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Error: Entity to remove doesn't exist in the current context");
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: Entity could not be removed from DB");
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException e)
            {
                throw new DataAccessException("Error: changes could not be applied to DB " + e.Message);
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: changes could not be applied to DB");
            }
        }

        public void Update(T entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.Set<T>().Update(entity);
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Error: Entity to update doesn't exist in the current context");
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: Could not update Entity in DB");
            }
        }

       
    }
}
