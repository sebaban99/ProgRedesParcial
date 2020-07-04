using Iemedebe.DataAccess;
using Iemedebe.Domain;
using Iemedebe.BusinessLogic.Interfaces;
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
            this.directorValidator = directorValidator;
            this.directorRepository = directorRepository;
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
            throw new NotImplementedException();
        }

        public async Task<Director> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Director> GetByConditionAsync(Expression<Func<Director, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Director entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Director> UpdateAsync(Director modifiedEntity, Director originalEntity)
        {
            throw new NotImplementedException();
        }
    }
}
