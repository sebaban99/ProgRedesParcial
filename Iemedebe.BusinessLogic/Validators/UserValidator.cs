using Iemedebe.BusinessLogic.Exceptions;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public class UserValidator : IUserValidator<User>
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Film> filmRepository;

        public UserValidator(IRepository<User> userRepository, IRepository<Film> filmRepository)
        {
            this.userRepository = userRepository;
            this.filmRepository = filmRepository;
        }

        public async Task ValidateAddFavouriteFilmAsync(User entity, Film film)
        {
            await ValidateExistsAsync(entity).ConfigureAwait(false);
            await ValidateFilmExistsAsync(film).ConfigureAwait(false);
            ValidateUserDoesNotHaveFavouriteFilm(entity, film);
        }

        public async Task ValidateDeleteFavouriteFilmAsync(User entity, Film film)
        {
            await ValidateExistsAsync(entity).ConfigureAwait(false);
            await ValidateFilmExistsAsync(film).ConfigureAwait(false);
            ValidateUserHasFavouriteFilm(entity, film);
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
        private async Task ValidateFilmExistsAsync(Film film)
        {
            if (!await Exists(film).ConfigureAwait(false))
            {
                throw new BusinessLogicException("Genre not found");
            }
        }

        private async Task<bool> Exists(Film film)
        {
            try
            {
                return await filmRepository.GetByConditionAsync(s => s.Name == film.Name).ConfigureAwait(false) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ValidateUserDoesNotHaveFavouriteFilm(User user, Film film)
        {
            throw new NotImplementedException();
        }

        private void ValidateUserHasFavouriteFilm(User user, Film film)
        {
            throw new NotImplementedException();
        }

    }
}
