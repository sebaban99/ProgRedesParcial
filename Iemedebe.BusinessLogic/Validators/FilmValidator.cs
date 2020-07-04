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
    public class FilmValidator : IValidator<Film>
    {
        private readonly IRepository<Film> filmRepository;

        public FilmValidator(IRepository<Film> filmRepository)
        {
            this.filmRepository = filmRepository;
        }

        public async Task ValidateAddAsync(Film entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (exists)
            {
                throw new BusinessLogicException("There is already a film with the same name\n");
            }
        }

        public async Task ValidateDeleteAsync(Film entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The film does not exist.");
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
            if (modifiedEntity.Name.Equals(originalEntity.Name))
            {

            }
            else if (await ExistsAsync(modifiedEntity).ConfigureAwait(false))
            {
                throw new BusinessLogicException("There is already a film with the same name\n");
            }
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
    }
}
