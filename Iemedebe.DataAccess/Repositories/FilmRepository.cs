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
    public class FilmRepository : BaseRepository<Film>
    {
        public FilmRepository(DbContext context)
        {
            Context = context;
        }

        public override async Task<Film> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<Film>().FirstAsync(x => x.Id == id).ConfigureAwait(false);
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
