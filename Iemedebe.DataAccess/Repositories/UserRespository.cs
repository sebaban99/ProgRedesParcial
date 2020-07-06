using Iemedebe.Domain;
using Iemedebe.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.DataAccess
{
    public class UserRespository : BaseRepository<User>
    {
        public UserRespository(DbContext context)
        {
            Context = context;
        }

        public override async Task<User> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<User>().Include(u => u.FavouriteFilms).FirstAsync(x => x.Id == id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error: could not find the specified Entity");
            }
        }
    }
}
