using Iemedebe.BusinessLogic.Exceptions;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public class FilmValidator : IFilmValidator<Film>
    {
        private readonly IRepository<Film> filmRepository;
        private readonly IRepository<Genre> genreRepository;

        public FilmValidator(IRepository<Film> filmRepository, IRepository<Genre> genreRepository)
        {
            this.filmRepository = filmRepository;
            this.genreRepository = genreRepository;
        }

        public async Task ValidateAddAsync(Film entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (exists)
            {
                throw new BusinessLogicException("There is already a film with the same name\n");
            }
        }

        public async Task ValidateAddGenreAsync(Film entity, Genre genre)
        {
            await ValidateExistsAsync(entity).ConfigureAwait(false);
            await ValidateGenreExistsAsync(genre).ConfigureAwait(false);
            ValidateFilmDoesNotAlreadyContainGenre(entity, genre);
        }

        public async Task ValidateDeleteAsync(Film entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The film does not exist.");
            }
        }
        public async Task ValidateDeleteGenreAsync(Film entity, Genre genre)
        {
            await ValidateExistsAsync(entity).ConfigureAwait(false);
            await ValidateGenreExistsAsync(genre).ConfigureAwait(false);
            ValidateFilmContainsGenre(entity, genre);

        }

        public async Task ValidateExistsAsync(Film entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The film does not exist.");
            }
        }

        public async Task ValidateUpdateAsync(Film modifiedEntity, Film originalEntity)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> ExistsAsync(Film entity)
        {
            try
            {
                return await filmRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task ValidateGenreExistsAsync(Genre genre)
        {
            if (!await Exists(genre).ConfigureAwait(false))
            {
                throw new BusinessLogicException("Genre not found");
            }
        }

        private async Task<bool> Exists(Genre genre)
        {
            try
            {
                return await genreRepository.GetByConditionAsync(s => s.Name == genre.Name).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ValidateFilmDoesNotAlreadyContainGenre(Film film, Genre genre)
        {
            throw new NotImplementedException();
        }

        private void ValidateFilmContainsGenre(Film film, Genre genre)
        {
            throw new NotImplementedException();
        }

    }
}
