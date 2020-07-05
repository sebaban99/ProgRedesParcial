using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Iemedebe.Domain;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface IFilmValidator<T> : IValidator<T>
    {
        Task ValidateAddGenreAsync(T entity, Genre genre);
        Task ValidateDeleteGenreAsync(T entity, Genre genre);

    }
}
