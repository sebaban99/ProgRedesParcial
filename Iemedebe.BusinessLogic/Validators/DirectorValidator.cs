using Iemedebe.BusinessLogic.Exceptions;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public class DirectorValidator : IValidator<Director>
    {
        private readonly IRepository<Director> directorRepository;

        public DirectorValidator(IRepository<Director> directorRepository)
        {
            this.directorRepository = directorRepository;
        }

        public async Task ValidateAddAsync(Director entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (exists)
            {
                throw new BusinessLogicException("There is already a director with the same name\n");
            }
        }

        public async Task ValidateDeleteAsync(Director entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The director does not exist.");
            }
        }

        public async Task ValidateExistsAsync(Director entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The director does not exist.");
            }
        }

        public async Task ValidateUpdateAsync(Director modifiedEntity, Director originalEntity)
        {
            if (!await ExistsAsync(originalEntity).ConfigureAwait(false))
            {
                throw new BusinessLogicException("Error: The director you are trying to update does not exist");
            }
            else
            {
                if (modifiedEntity.Name != originalEntity.Name)
                {
                    if (await ExistsAsync(modifiedEntity).ConfigureAwait(false))
                    {
                        throw new BusinessLogicException("Error: A director with the same name already exists");
                    }
                }
            }
        }

        private async Task<bool> ExistsAsync(Director entity)
        {
            try
            {
                return await directorRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
