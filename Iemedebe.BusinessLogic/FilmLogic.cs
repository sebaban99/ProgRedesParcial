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
        private readonly IFilmValidator<Film> filmValidator;
        private readonly IRepository<Film> filmRepository;
        private readonly IRepository<FilmWithGenre> filmWithGenreRepo;
        private readonly IRepository<Genre> genreRepository;
        private readonly IRepository<Director> directorRepository;

        public FilmLogic(IRepository<Film> filmRepository, IFilmValidator<Film> filmValidator,
            IRepository<FilmWithGenre> filmWithGenreRepo, IRepository<Genre> genreRepository, IRepository<Director> directorRepository)
        {
            this.filmRepository = filmRepository;
            this.filmValidator = filmValidator;
            this.filmWithGenreRepo = filmWithGenreRepo;
            this.genreRepository = genreRepository;
            this.directorRepository = directorRepository;
        }

        public async Task<Film> AddGenreAsync(Film film, Genre genre)
        {
            try
            {
                var filmToUpdate = await filmRepository.GetByConditionAsync(s => s.Name == film.Name).ConfigureAwait(false);
                await filmValidator.ValidateAddGenreAsync(filmToUpdate, genre).ConfigureAwait(false);
                FilmWithGenre newAssocistion = new FilmWithGenre()
                {
                    Id = Guid.NewGuid(),
                    Film = film,
                    FilmId = film.Id,
                    Genre = genre,
                    GenreId = genre.Id
                };
                filmWithGenreRepo.Add(newAssocistion);
                film.Genres.Add(newAssocistion);
                genre.FilmsAssociated.Add(newAssocistion);
                genreRepository.Update(genre);
                filmRepository.Update(film);
                await filmRepository.SaveChangesAsync().ConfigureAwait(false);

                return await filmRepository.GetAsync(filmToUpdate.Id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }

        public Task<Film> AddRatingAsync(Rating rating)
        {
            throw new NotImplementedException();
        }

        private void FormatFilm(Film filmToAdd)
        {
            filmToAdd.Id = Guid.NewGuid();
            filmToAdd.Genres[0].FilmId = filmToAdd.Id;
            filmToAdd.AdditionDate = DateTime.Now;
        }

        public async Task<Film> CreateAsync(Film filmToAdd)
        {
            try
            {
                await filmValidator.ValidateAddAsync(filmToAdd).ConfigureAwait(false);
                FormatFilm(filmToAdd);
                filmRepository.Add(filmToAdd);
                await filmRepository.SaveChangesAsync().ConfigureAwait(false);
                return filmToAdd;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
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
                throw new BusinessLogicException(e.Message);
            }
        }

        public async Task<Film> RemoveGenreAsync(Film film, Genre genre)
        {
            try
            {
                var filmToUpdate = await filmRepository.GetByConditionAsync(s => s.Name == film.Name).ConfigureAwait(false);
                await filmValidator.ValidateDeleteGenreAsync(filmToUpdate, genre);
                var associationToRemove = await filmWithGenreRepo.GetByConditionAsync(f => f.GenreId == genre.Id && f.FilmId == film.Id).ConfigureAwait(false);
                
                filmWithGenreRepo.Remove(associationToRemove);
                film.Genres.Remove(associationToRemove);
                genre.FilmsAssociated.Remove(associationToRemove);
                genreRepository.Update(genre);
                filmRepository.Update(film);

                await filmRepository.SaveChangesAsync().ConfigureAwait(false);

                return await filmRepository.GetAsync(filmToUpdate.Id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
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
                await filmValidator.ValidateUpdateAsync(modifiedEntity, filmToUpdate).ConfigureAwait(false);
                
                var director = await directorRepository.GetAsync(modifiedEntity.Director.Id).ConfigureAwait(false);
                filmToUpdate.Name = modifiedEntity.Name;
                filmToUpdate.Description = modifiedEntity.Description;
                filmToUpdate.LaunchDate = modifiedEntity.LaunchDate;
                filmToUpdate.Director = director;

                filmRepository.Update(modifiedEntity);
                await filmRepository.SaveChangesAsync().ConfigureAwait(false);
                return modifiedEntity;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }
    }
}
