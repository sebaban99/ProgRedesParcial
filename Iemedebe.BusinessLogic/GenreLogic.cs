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
    public class GenreLogic : ILogic<Genre>
    {
        public async Task<Genre> CreateAsync(Genre entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Genre> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Genre> GetByConditionAsync(Expression<Func<Genre, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Genre entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Genre> UpdateAsync(Genre modifiedEntity, Genre originalEntity)
        {
            throw new NotImplementedException();
        }
    }
}
