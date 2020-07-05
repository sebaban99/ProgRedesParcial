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
    public class DirectorLogic : ILogic<Director>
    {
        private readonly IValidator<Director> directorValidator;
        private readonly IRepository<Director> directorRepository;

        public DirectorLogic(IRepository<Director> directorRepository, IValidator<Director> directorValidator)
        {
            this.directorRepository = directorRepository;
            this.directorValidator = directorValidator;
        }

        public async Task<Director> CreateAsync(Director entity)
        {
            try
            {
                await directorValidator.ValidateAddAsync(entity).ConfigureAwait(false);
                directorRepository.Add(entity);
                await directorRepository.SaveChangesAsync().ConfigureAwait(false);
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Director>> GetAllAsync()
        {
            return await directorRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Director> GetAsync(Guid id)
        {
            return await directorRepository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<Director> GetByConditionAsync(Expression<Func<Director, bool>> expression)
        {
            return await directorRepository.GetByConditionAsync(expression).ConfigureAwait(false);
        }

        public async Task RemoveAsync(Director entity)
        {
            try
            {
                var directorToRemove = await directorRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false);
                directorRepository.Remove(directorToRemove);
                await directorRepository.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Error: Invalid director.");
            }
        }

        public async Task<Director> UpdateAsync(Director modifiedEntity, Director originalEntity)
        {
            try
            {
                var directorToUpdate = await directorRepository.GetByConditionAsync(s => s.Name == originalEntity.Name).ConfigureAwait(false);
                modifiedEntity.Id = directorToUpdate.Id;
                await directorValidator.ValidateUpdateAsync(modifiedEntity, directorToUpdate).ConfigureAwait(false);
                directorRepository.Update(modifiedEntity);
                await directorRepository.SaveChangesAsync().ConfigureAwait(false);
                return modifiedEntity;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
