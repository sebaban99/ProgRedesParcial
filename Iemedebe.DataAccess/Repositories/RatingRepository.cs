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
    public class RatingRepository : BaseRepository<Rating>
    {
        public RatingRepository(DbContext context)
        {
            Context = context;
        }

        public override async Task<Rating> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<Rating>().Include(r => r.RatedBy)
                                                  .Include(r => r.RatedFilm)
                                                  .FirstAsync(x => x.Id == id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error: could not find the specified Entity");
            }
        }
    }
}
