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
            if(entity.Director == null || entity.Genres.Count == 0)
            {
                throw new BusinessLogicException("Error: Film needs to have one director and at least one genre to be added successfully\n");
            }
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
            await ValidateFilmDoesNotAlreadyContainGenre(entity, genre).ConfigureAwait(false);
        }

        public async Task ValidateDeleteAsync(Film entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The film does not exist.");
            }
        }
        private bool ValidateFilmHasOnlyOneGenre(Film film)
        {
            return film.Genres.Count == 1;
        }

        public async Task ValidateDeleteGenreAsync(Film entity, Genre genre)
        {
            await ValidateExistsAsync(entity).ConfigureAwait(false);
            await ValidateGenreExistsAsync(genre).ConfigureAwait(false);
            await ValidateFilmContainsGenreAsync(entity, genre).ConfigureAwait(false);
            if (ValidateFilmHasOnlyOneGenre(entity))
            {
                throw new BusinessLogicException("Error: Can't delete a genre from a movie if has only one genre");
            }
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
            if(!await ExistsAsync(originalEntity).ConfigureAwait(false))
            {
                throw new BusinessLogicException("Error: The film you are trying to update does not exist");
            }
            else
            {
                if(modifiedEntity.Name != originalEntity.Name)
                {
                    if(await ExistsAsync(modifiedEntity).ConfigureAwait(false))
                    {
                        throw new BusinessLogicException("Error: A film with the same name already exists");
                    }
                }
            }
        }

        private async Task<bool> ExistsAsync(Film entity)
        {
            try
            {
                return await filmRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Error: Could not ");
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

        private async Task ValidateFilmDoesNotAlreadyContainGenre(Film film, Genre genre)
        {
            var filmInDB = await filmRepository.GetAsync(film.Id).ConfigureAwait(false);
            if (ContainsGenre(filmInDB.Genres, genre.Id))
            {
                throw new BusinessLogicException("Error: The genre you are trying to add to film is already associated with this film");
            }
        }

        private bool ContainsGenre(List<FilmWithGenre> list, Guid idGenre)
        {
            foreach (FilmWithGenre filmWithGenre in list)
            {
                if(filmWithGenre.GenreId == idGenre)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task ValidateFilmContainsGenreAsync(Film film, Genre genre)
        {
            var filmInDB = await filmRepository.GetAsync(film.Id).ConfigureAwait(false);
            if(!ContainsGenre(filmInDB.Genres, genre.Id))
            {
                throw new BusinessLogicException("Error: The genre you are trying to delete from film is no associated to this film");
            }
        }

    }
}
