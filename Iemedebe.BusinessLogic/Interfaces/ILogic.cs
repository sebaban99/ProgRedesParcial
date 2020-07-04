using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface ILogic<T>
    {
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression);

        Task<T> CreateAsync(T entity);

        Task RemoveAsync(T entity);

        Task<T> UpdateAsync(T modifiedEntity, T originalEntity);

        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);
    }
}
