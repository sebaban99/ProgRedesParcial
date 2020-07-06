using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.DataAccess
{
    public interface IRepository<T>
    {
        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);

        Task<T> GetAsync(Guid id);

        Task<List<T>> GetAllAsync();

        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAllByConditionAsync(Expression<Func<T, bool>> expression);

        Task SaveChangesAsync();

    }
}
