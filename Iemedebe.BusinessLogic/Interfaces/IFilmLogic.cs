using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Iemedebe.Domain;

namespace Iemedebe.BusinessLogic
{
    public interface IFilmLogic<T> : ILogic<T>
    {
        Task<T> AddGenreAsync(T entity, Genre genre);
        Task<T> RemoveGenreAsync(T entity, Genre genre);
    }
}
