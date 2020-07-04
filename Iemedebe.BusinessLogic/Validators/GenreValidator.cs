using Iemedebe.BusinessLogic.Interfaces;
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

        public GenreValidator(IRepository<Genre> genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task ValidateAddAsync(Genre entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (exists)
            {
                throw new BusinessLogicException("There is already a genre with the same name\n");
            }
        }

        public async Task ValidateDeleteAsync(Genre entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The genre does not exist.");
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
            throw new NotImplementedException();
        }

        private async Task<bool> ExistsAsync(Genre entity)
        {
            try
            {
                return await genreRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
