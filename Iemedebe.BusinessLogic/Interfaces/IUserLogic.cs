using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface IUserLogic<T> : ILogic<T>
    {
        Task<T> AddFavouriteAsync(UserFavouriteFilm favourite);
        Task<T> RemoveFavouriteAsync(Guid id, Guid favouriteId);
    }
}
