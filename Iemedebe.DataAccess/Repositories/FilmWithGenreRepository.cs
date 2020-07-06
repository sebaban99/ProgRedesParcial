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
    public class FilmWithGenreRepository : BaseRepository<FilmWithGenre>
    {
        public FilmWithGenreRepository(DbContext context)
        {
            Context = context;
        }

        public override async Task<FilmWithGenre> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<FilmWithGenre>().FirstAsync(x => x.Id == id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error: could not find the specified Entity");
            }
        }
       
    }
}
