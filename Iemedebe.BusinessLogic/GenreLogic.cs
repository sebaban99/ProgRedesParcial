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
    public class GenreLogic : ILogic<Genre>
    {
        private readonly IValidator<Genre> genreValidator;
        private readonly IRepository<Genre> genreRepository;

        public GenreLogic(IRepository<Genre> genreRepository, IValidator<Genre> genreValidator)
        {
            this.genreRepository = genreRepository;
            this.genreValidator = genreValidator;
        }

        public async Task<Genre> CreateAsync(Genre entity)
        {
            try
            {
                await genreValidator.ValidateAddAsync(entity).ConfigureAwait(false);
                genreRepository.Add(entity);
                await genreRepository.SaveChangesAsync().ConfigureAwait(false);
                return entity;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await genreRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Genre> GetAsync(Guid id)
        {
            return await genreRepository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<Genre> GetByConditionAsync(Expression<Func<Genre, bool>> expression)
        {
            return await genreRepository.GetByConditionAsync(expression).ConfigureAwait(false);
        }

        public async Task RemoveAsync(Genre entity)
        {
            try
            {
                var genreToRemove = await genreRepository.GetByConditionAsync(s => s.Name == entity.Name).ConfigureAwait(false);
                genreRepository.Remove(genreToRemove);
                await genreRepository.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }

        public async Task<Genre> UpdateAsync(Genre modifiedEntity, Genre originalEntity)
        {
            try
            {
                var genreToUpdate = await genreRepository.GetByConditionAsync(s => s.Name == originalEntity.Name).ConfigureAwait(false);
                await genreValidator.ValidateUpdateAsync(modifiedEntity, genreToUpdate).ConfigureAwait(false);

                genreToUpdate.Name = modifiedEntity.Name;
                genreToUpdate.Description = modifiedEntity.Description;

                genreRepository.Update(genreToUpdate);
                await genreRepository.SaveChangesAsync().ConfigureAwait(false);
                return modifiedEntity;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }
    }
}
