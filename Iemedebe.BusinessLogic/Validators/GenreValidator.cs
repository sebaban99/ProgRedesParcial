using Iemedebe.BusinessLogic.Exceptions;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public class GenreValidator : IValidator<Genre>
    {
        private readonly IRepository<Genre> genreRepository;
        private readonly IRepository<Film> filmRepository;
        private readonly IRepository<FilmWithGenre> fwgRepository;

        public GenreValidator(IRepository<Genre> genreRepository, IRepository<Film> filmRepository, 
            IRepository<FilmWithGenre> fwgRepository)
        {
            this.fwgRepository = fwgRepository;
            this.genreRepository = genreRepository;
            this.filmRepository = filmRepository;
        }

        public async Task ValidateAddAsync(Genre entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (exists)
            {
                throw new BusinessLogicException("Error: There is already a genre with the same name\n");
            }
        }

        public async Task ValidateDeleteAsync(Genre entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The genre does not exist.");
            }
            var filmsWithGenre = await fwgRepository.GetAllByConditionAsync(f => f.GenreId == entity.Id).ConfigureAwait(false);
            foreach(FilmWithGenre f in filmsWithGenre)
            {
                var filmInDB = await filmRepository.GetAsync(f.FilmId).ConfigureAwait(false);
                if(filmInDB.Genres.Count == 1)
                {
                    throw new BusinessLogicException("Error: Can't delete this genre because it would leave a movie without genres");
                }
            }
        }

        public async Task ValidateExistsAsync(Genre entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The genre does not exist.");
            }
        }

        public async Task ValidateUpdateAsync(Genre modifiedEntity, Genre originalEntity)
        {
            if (!await ExistsAsync(originalEntity).ConfigureAwait(false))
            {
                throw new BusinessLogicException("Error: The genre you are trying to update does not exist");
            }
            else
            {
                if (modifiedEntity.Name != originalEntity.Name)
                {
                    if (await ExistsAsync(modifiedEntity).ConfigureAwait(false))
                    {
                        throw new BusinessLogicException("Error: A genre with the same name already exists");
                    }
                }
            }
        }

        private async Task<bool> ExistsAsync(Genre entity)
        {
            try
            {
                return await genreRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }
    }
}
