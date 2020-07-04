using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface IFilmLogic<T> : ILogic<T>
    {
        Task<T> AddGenreAsync(T entity, Genre genre);

        Task<T> RemoveGenreAsync(T entity, Genre genre);

        Task<T> AddRatingAsync(Rating rating);

        Task<T> PutRatingAsync(Guid idFilm, Rating rating);

        Task RemoveRatingAsync(Guid idFilm, Guid idRating);
    }
}
