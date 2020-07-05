using Iemedebe.DataAccess;
using Iemedebe.Domain;
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
        private readonly IValidator<Film> filmValidator;
        private readonly IRepository<Film> filmRepository;

        public FilmLogic(IRepository<Film> filmRepository, IValidator<Film> filmValidator)
        {
            this.filmRepository = filmRepository;
            this.filmValidator = filmValidator;
        }

        public async Task<Film> AddGenreAsync(Film entity, Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<Film> AddRatingAsync(Rating rating)
        {
            throw new NotImplementedException();
        }

        public async Task<Film> CreateAsync(Film entity)
        {
            try
            {
                await filmValidator.ValidateAddAsync(entity).ConfigureAwait(false);
                filmRepository.Add(entity);
                await filmRepository.SaveChangesAsync().ConfigureAwait(false);
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Film>> GetAllAsync()
        {
            return await filmRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Film> GetAsync(Guid id)
        {
            return await filmRepository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<Film> GetByConditionAsync(Expression<Func<Film, bool>> expression)
        {
            return await filmRepository.GetByConditionAsync(expression).ConfigureAwait(false);
        }

        public Task<Film> PutRatingAsync(Guid idFilm, Rating rating)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Film entity)
        {
            try
            {
                var filmToRemove = await filmRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false);
                filmRepository.Remove(filmToRemove);
                await filmRepository.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Error: Invalid film.");
            }
        }

        public async Task<Film> RemoveGenreAsync(Film entity, Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRatingAsync(Guid idFilm, Guid idRating)
        {
            throw new NotImplementedException();
        }

        public async Task<Film> UpdateAsync(Film modifiedEntity, Film originalEntity)
        {
            try
            {
                var filmToUpdate = await filmRepository.GetByConditionAsync(s => s.Name == originalEntity.Name).ConfigureAwait(false);
                modifiedEntity.Id = filmToUpdate.Id;
                await filmValidator.ValidateUpdateAsync(modifiedEntity, filmToUpdate).ConfigureAwait(false);
                filmRepository.Update(modifiedEntity);
                await filmRepository.SaveChangesAsync().ConfigureAwait(false);
                return modifiedEntity;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
