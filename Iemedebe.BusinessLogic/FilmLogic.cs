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
        private readonly IRatingLogic<Rating> ratingLogic;

        public FilmLogic(IRepository<Film> filmRepository, IFilmValidator<Film> filmValidator,
            IRepository<FilmWithGenre> filmWithGenreRepo, IRepository<Genre> genreRepository, IRepository<Director> directorRepository,
           IRatingLogic<Rating> ratingLogic)
        {
            this.filmRepository = filmRepository;
            this.filmValidator = filmValidator;
            this.filmWithGenreRepo = filmWithGenreRepo;
            this.genreRepository = genreRepository;
            this.directorRepository = directorRepository;
            this.ratingLogic = ratingLogic;
        }

        public async Task<Film> AddGenreAsync(Film film, Genre genre)
        {
            try
            {
                var filmToUpdate = await filmRepository.GetByConditionAsync(s => s.Name == film.Name).ConfigureAwait(false);
                await filmValidator.ValidateAddGenreAsync(filmToUpdate, genre).ConfigureAwait(false);
                FilmWithGenre newAssocistion = new FilmWithGenre()
                {
                    Film = film,
                    FilmId = film.Id,
                    Genre = genre,
                    GenreId = genre.Id
                };
                filmWithGenreRepo.Add(newAssocistion);
                filmRepository.Update(filmToUpdate);
                await filmRepository.SaveChangesAsync().ConfigureAwait(false);

                foreach(FilmWithGenre fwg in filmToUpdate.Genres)
                {
                    fwg.Genre = await genreRepository.GetAsync(fwg.GenreId).ConfigureAwait(false);
                }
                return filmToUpdate;
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

        private int CalculateScore(Film film)
        {
            if (film.Ratings.Count == 0)
            {
                return 0;
            }
            int totalScores = 0;
            foreach(Rating r in film.Ratings)
            {
                totalScores += r.Score;
            }
            return totalScores / film.Ratings.Count;
        }

        public async Task<Film> AddRatingAsync(Rating rating)
        {
            var ratingCreated = await ratingLogic.CreateAsync(rating).ConfigureAwait(false);
            var filmInDB = await filmRepository.GetAsync(ratingCreated.RatedFilm.Id).ConfigureAwait(false);
            filmInDB.Ratings.Add(ratingCreated);
            filmInDB.FilmScore = CalculateScore(filmInDB);
            filmRepository.Update(filmInDB);
            await filmRepository.SaveChangesAsync().ConfigureAwait(false);

            return filmInDB;
        }

        public async Task RemoveRatingAsync(Guid idFilm, Guid idRating)
        {
            var ratingInDB = await ratingLogic.GetAsync(idRating).ConfigureAwait(false);
            await ratingLogic.RemoveAsync(ratingInDB).ConfigureAwait(false);
            var filmInDB = await filmRepository.GetAsync(idFilm).ConfigureAwait(false);
            filmInDB.Ratings.Remove(ratingInDB);
            filmInDB.FilmScore = CalculateScore(filmInDB);
            filmRepository.Update(filmInDB);
            await filmRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Film> PutRatingAsync(Guid idRating, int score)
        {
            var ratingUpdated = await ratingLogic.UpdateRatingAsync(idRating, score);
            var filmInDB = await filmRepository.GetAsync(ratingUpdated.RatedFilm.Id).ConfigureAwait(false);
            filmInDB.FilmScore = CalculateScore(filmInDB);
            filmRepository.Update(filmInDB);
            await filmRepository.SaveChangesAsync().ConfigureAwait(false);

            return filmInDB;
        }

        private void FormatFilm(Film filmToAdd)
        {
            filmToAdd.Id = Guid.NewGuid();
            filmToAdd.Genres[0].FilmId = filmToAdd.Id;
            filmToAdd.Genres[0].Id = Guid.NewGuid();
            filmToAdd.AdditionDate = DateTime.Now;
        }

        public async Task<Film> CreateAsync(Film filmToAdd)
        {
            try
            {
                await filmValidator.ValidateAddAsync(filmToAdd).ConfigureAwait(false);
                FormatFilm(filmToAdd);
                filmWithGenreRepo.Add(filmToAdd.Genres[0]);
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
            var filmsInDB =  await filmRepository.GetAllAsync().ConfigureAwait(false);
            foreach(Film f in filmsInDB)
            {
                foreach (FilmWithGenre fwg in f.Genres)
                {
                    fwg.Genre = await genreRepository.GetAsync(fwg.GenreId).ConfigureAwait(false);
                }
            }
            return filmsInDB;
        }

        public async Task<Film> GetAsync(Guid id)
        {
            return await filmRepository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<Film> GetByConditionAsync(Expression<Func<Film, bool>> expression)
        {
            return await filmRepository.GetByConditionAsync(expression).ConfigureAwait(false);
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
       

        public async Task<Film> UpdateAsync(Film modifiedEntity, Film originalEntity)
        {
            try
            {
                var filmToUpdate = await filmRepository.GetAsync(originalEntity.Id).ConfigureAwait(false);
                await filmValidator.ValidateUpdateAsync(modifiedEntity, filmToUpdate).ConfigureAwait(false);
                
                var director = await directorRepository.GetAsync(modifiedEntity.Director.Id).ConfigureAwait(false);
                filmToUpdate.Name = modifiedEntity.Name;
                filmToUpdate.Description = modifiedEntity.Description;
                filmToUpdate.LaunchDate = modifiedEntity.LaunchDate;
                filmToUpdate.Director = director;
                filmToUpdate.AdditionDate = filmToUpdate.AdditionDate;

                filmRepository.Update(filmToUpdate);
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
