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
    public class RatingValidator : IValidator<Rating>
    {
        private readonly IRepository<Rating> ratingRepository;

        public RatingValidator(IRepository<Rating> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task ValidateAddAsync(Rating entity)
        {
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
            if (modifiedEntity.RatedFilm.Equals(originalEntity.RatedFilm) && modifiedEntity.RatedBy.Equals(originalEntity.RatedBy))
            {

            }
            else if (await ExistsAsync(modifiedEntity).ConfigureAwait(false))
            {
                throw new BusinessLogicException("There is already a rating for that movie\n");
            }
            throw new NotImplementedException();
        }

        private async Task<bool> ExistsAsync(Rating entity)
        {
            try
            {
                return await ratingRepository.GetByConditionAsync(s => s.RatedFilm == entity.RatedFilm && s.RatedBy == entity.RatedBy).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
