using Iemedebe.DataAccess;
using Iemedebe.Domain;
using Iemedebe.BusinessLogic.Interfaces;
using Iemedebe.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq.Expressions;

namespace Iemedebe.BusinessLogic
{
    public class FilmLogic : IFilmLogic<Film>
    {
        public async Task<Film> AddGenreAsync(Film entity, Genre genre)
        {
            throw new NotImplementedException();
        }

        public async Task<Film> CreateAsync(Film entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Film>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Film> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Film> GetByConditionAsync(Expression<Func<Film, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Film entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Film> RemoveGenreAsync(Film entity, Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<Film> UpdateAsync(Film modifiedEntity, Film originalEntity)
        {
            throw new NotImplementedException();
        }
    }
}
