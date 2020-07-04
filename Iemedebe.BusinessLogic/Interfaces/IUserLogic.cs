using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Iemedebe.Domain;

namespace Iemedebe.BusinessLogic
{
    public interface IUserLogic<T> : ILogic<T>
    {
        Task<T> AddFavouriteAsync(T entity, Film film);
        Task<T> RemoveFavouriteAsync(T entity, Film film);
    }
}
