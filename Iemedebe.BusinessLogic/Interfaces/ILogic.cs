using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface ILogic<T>
    {
        Task<T> GetByCondition(Expression<Func<T, bool>> expression);

        Task<T> Create(T entity);

        Task Remove(T entity);

        Task<T> Update(T entity);

        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
    }
}
