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
    public class UserLogic: IUserLogic<User>
    {
        private readonly IUserValidator<User> userValidator;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Film> filmRepository;

        public UserLogic(IRepository<User> userRepository, IUserValidator<User> userValidator, IRepository<Film> filmRepository)
        {
            this.userRepository = userRepository;
            this.filmRepository = filmRepository;
            this.userValidator = userValidator;
        }

        public async Task<User> AddFavouriteAsync(UserFavouriteFilm favourite)
        {
            try
            {
                var userToUpdate = await userRepository.GetAsync(favourite.UserId).ConfigureAwait(false);
                var filmToFavourite = await filmRepository.GetAsync(favourite.FilmId).ConfigureAwait(false);
                await userValidator.ValidateAddFavouriteFilmAsync(userToUpdate, filmToFavourite);
                // TODO: Add favourite
                await userRepository.SaveChangesAsync().ConfigureAwait(false);
                return await userRepository.GetAsync(userToUpdate.Id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> CreateAsync(User entity)
        {
            try
            {
                await userValidator.ValidateAddAsync(entity).ConfigureAwait(false);
                userRepository.Add(entity);
                await userRepository.SaveChangesAsync().ConfigureAwait(false);
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await userRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await userRepository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task<User> GetByConditionAsync(Expression<Func<User, bool>> expression)
        {
            return await userRepository.GetByConditionAsync(expression).ConfigureAwait(false);
        }

        public async Task RemoveAsync(User entity)
        {
            try
            {
                var userToRemove = await userRepository.GetByConditionAsync(s => s.Nickname == entity.Nickname).ConfigureAwait(false);
                userRepository.Remove(userToRemove);
                await userRepository.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Error: Invalid user.");
            }
        }

        public async Task<User> RemoveFavouriteAsync(Guid id, Guid favouriteId)
        {
            try
            {
                var userToUpdate = await userRepository.GetAsync(id).ConfigureAwait(false);
                var filmToFavourite = await filmRepository.GetAsync(favouriteId).ConfigureAwait(false);
                await userValidator.ValidateDeleteFavouriteFilmAsync(userToUpdate, filmToFavourite);
                // TODO: Remove favourite
                await userRepository.SaveChangesAsync().ConfigureAwait(false);
                return await userRepository.GetAsync(userToUpdate.Id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> UpdateAsync(User modifiedEntity, User originalEntity)
        {
            try
            {
                var userToUpdate = await userRepository.GetByConditionAsync(s => s.Nickname == originalEntity.Nickname).ConfigureAwait(false);
                modifiedEntity.Id = userToUpdate.Id;
                await userValidator.ValidateUpdateAsync(modifiedEntity, userToUpdate).ConfigureAwait(false);
                userRepository.Update(modifiedEntity);
                await userRepository.SaveChangesAsync().ConfigureAwait(false);
                return modifiedEntity;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
