using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface IUserValidator<T> : IValidator<T>
    {
        Task ValidateAddFavouriteFilmAsync(T entity, Film film);
        Task ValidateDeleteFavouriteFilmAsync(T entity, Film film);
    }
}
