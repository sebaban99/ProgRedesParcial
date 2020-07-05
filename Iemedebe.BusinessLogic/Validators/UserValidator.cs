using Iemedebe.BusinessLogic.Exceptions;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public class UserValidator : IValidator<User>
    {
        private readonly IRepository<User> userRepository;

        public UserValidator(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task ValidateAddAsync(User entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (exists)
            {
                throw new BusinessLogicException("There is already a user with the same nickname\n");
            }
        }

        public async Task ValidateDeleteAsync(User entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The user does not exist.");
            }
        }

        public async Task ValidateExistsAsync(User entity)
        {
            var exists = await ExistsAsync(entity).ConfigureAwait(false);
            if (!exists)
            {
                throw new BusinessLogicException("The user does not exist.");
            }
        }

        public async Task ValidateUpdateAsync(User modifiedEntity, User originalEntity)
        {
            if (modifiedEntity.Nickname.Equals(originalEntity.Nickname))
            {

            } else if (await ExistsAsync(modifiedEntity).ConfigureAwait(false))
            {
                throw new BusinessLogicException("There is already a user with the same nickname\n");
            }
        }

        private async Task<bool> ExistsAsync(User user)
        {
            try
            {
                return await userRepository.GetByConditionAsync(s => s.Nickname == user.Nickname).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
