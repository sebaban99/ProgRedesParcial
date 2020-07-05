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
                return await Context.Set<Rating>().FirstAsync(x => x.Id == id).ConfigureAwait(false);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
            catch (DbException)
            {
                throw new DataAccessException("Error: could not get specific Entity");
            }
        }
    }
}
