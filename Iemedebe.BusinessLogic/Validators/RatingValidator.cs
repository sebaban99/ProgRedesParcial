using Iemedebe.BusinessLogic.Exceptions;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public class RatingValidator : IValidator<Rating>
    {
        private readonly IRepository<Rating> ratingRepository;

        public RatingValidator(IRepository<Rating> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        private bool IsScoreValid(int score)
        {
            return score >= 1 && score <= 5;
        }

        public async Task ValidateAddAsync(Rating entity)
        {
            if(!IsScoreValid(entity.Score)){
                throw new BusinessLogicException("Error: Score must be between 1 and 5");
            }
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (exists)
            {
                throw new BusinessLogicException("There is already a rating with the same name\n");
            }
        }

        public async Task ValidateDeleteAsync(Rating entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The rating does not exist.");
            }
        }

        public async Task ValidateExistsAsync(Rating entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The rating does not exist.");
            }
        }

        public async Task ValidateUpdateAsync(Rating modifiedEntity, Rating originalEntity)
        {
            if (!IsScoreValid(modifiedEntity.Score))
            {
                throw new BusinessLogicException("Error: Score must be between 1 and 5");
            }
            var exists = await ExistsAsync(modifiedEntity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("Error: The rating to update does not exist");
            }
        }

        private async Task<bool> ExistsAsync(Rating entity)
        {
            try
            {
                return await ratingRepository.GetByConditionAsync(s => s.RatedFilm.Id == entity.RatedFilm.Id && s.RatedBy.Id == entity.RatedBy.Id).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
