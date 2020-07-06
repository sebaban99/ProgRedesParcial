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
    public class UserFavouriteFilmRepository : BaseRepository<UserFavouriteFilm>
    {
        public UserFavouriteFilmRepository(DbContext context)
        {
            Context = context;
        }

        public override async Task<UserFavouriteFilm> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<UserFavouriteFilm>().Include(u => u.Film)
                                                             .Include(u => u.User)
                                                            .FirstAsync(x => x.Id == id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error: could not find the specified Entity");
            }
        }
    }
}
