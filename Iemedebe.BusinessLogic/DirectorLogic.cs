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
        public Task<Director> CreateAsync(Director entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Director>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Director> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Director> GetByConditionAsync(Expression<Func<Director, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Director entity)
        {
            throw new NotImplementedException();
        }

        public Task<Director> UpdateAsync(Director modifiedEntity, Director originalEntity)
        {
            throw new NotImplementedException();
        }
    }
}
