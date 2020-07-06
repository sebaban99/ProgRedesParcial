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
    public class RatingLogic : ILogic<Rating>
    {
        private readonly IValidator<Rating> ratingValidator;
        private readonly IRepository<Rating> ratingRepository;

        public RatingLogic(IRepository<Rating> ratingRepository, IValidator<Rating> ratingValidator)
        {
            this.ratingRepository = ratingRepository;
            this.ratingValidator = ratingValidator;
        }

        public async Task<Rating> CreateAsync(Rating entity)
        {
            try
            {
                await ratingValidator.ValidateAddAsync(entity).ConfigureAwait(false);
                entity.Id = Guid.NewGuid();
                ratingRepository.Add(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }

        public async Task<List<Rating>> GetAllAsync()
        {
            return await ratingRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Rating> GetAsync(Guid id)
        {
            return await ratingRepository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<Rating> GetByConditionAsync(Expression<Func<Rating, bool>> expression)
        {
            return await ratingRepository.GetByConditionAsync(expression).ConfigureAwait(false);
        }

        public async Task RemoveAsync(Rating entity)
        {
            try
            {
                var ratingToRemove = await ratingRepository.GetByConditionAsync(s => s.RatedBy.Nickname == entity.RatedBy.Nickname && s.RatedFilm.Name == entity.RatedFilm.Name).ConfigureAwait(false);
                ratingRepository.Remove(ratingToRemove);
                await ratingRepository.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }

        public async Task<Rating> UpdateAsync(Rating modifiedEntity, Rating originalEntity)
        {
            try
            {
                var ratingToUpdate = await ratingRepository.GetByConditionAsync(s => s.RatedBy.Nickname == originalEntity.RatedBy.Nickname && s.RatedFilm.Name == originalEntity.RatedFilm.Name).ConfigureAwait(false);
                modifiedEntity.Id = ratingToUpdate.Id;
                await ratingValidator.ValidateUpdateAsync(modifiedEntity, ratingToUpdate).ConfigureAwait(false);
                ratingRepository.Update(modifiedEntity);
                await ratingRepository.SaveChangesAsync().ConfigureAwait(false);
                return modifiedEntity;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }
    }
}
