using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface IValidator<T>
    {
        Task ValidateAddAsync(T entity);

        Task ValidateDeleteAsync(T entity);

        Task ValidateUpdateAsync(T modifiedEntity, T originalEntity);

        Task ValidateExistsAsync(T entity);
    }
}
