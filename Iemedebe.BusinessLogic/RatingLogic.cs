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
    public class RatingLogic : IRatingLogic<Rating>
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
            var ratingInDB = await ratingRepository.GetAsync(id).ConfigureAwait(false);
            if(ratingInDB == null)
            {
                throw new BusinessLogicException("Error: Could not find specified rating");
            }
            return ratingInDB;
        }

        public async Task<Rating> GetByConditionAsync(Expression<Func<Rating, bool>> expression)
        {
            return await ratingRepository.GetByConditionAsync(expression).ConfigureAwait(false);
        }

        public async Task RemoveAsync(Rating entity)
        {
            try
            {
                var ratingToRemove = await ratingRepository.GetByConditionAsync(s => s.RatedBy.Id == entity.RatedBy.Id && s.RatedFilm.Id == entity.RatedFilm.Id).ConfigureAwait(false);
                if(ratingToRemove == null)
                {
                    throw new BusinessLogicException("Error: Could not find rating to delete");
                }
                ratingRepository.Remove(ratingToRemove);
                await ratingRepository.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }

        public async Task<Rating> UpdateRatingAsync(Guid idRating, int score)
        {
            try
            {
                var ratingToUpdate = await ratingRepository.GetAsync(idRating).ConfigureAwait(false);
                ratingToUpdate.Score = score;
                await ratingValidator.ValidateUpdateAsync(ratingToUpdate, ratingToUpdate).ConfigureAwait(false);

                ratingRepository.Update(ratingToUpdate);
                await ratingRepository.SaveChangesAsync().ConfigureAwait(false);
                return ratingToUpdate;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(e.Message);
            }
        }

        public Task<Rating> UpdateAsync(Rating modifiedEntity, Rating originalEntity)
        {
            throw new NotImplementedException();
        }
    }
}
