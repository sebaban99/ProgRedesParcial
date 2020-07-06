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
    public class FilmFileRepository : BaseRepository<FilmFile>
    {
        public FilmFileRepository(DbContext context)
        {
            Context = context;
        }

        public override async Task<FilmFile> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<FilmFile>().FirstAsync(x => x.Id == id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error: could not find the specified Entity");
            }
        }

        public override async Task<List<FilmFile>> GetAllAsync()
        {
            try
            {
                return await Context.Set<FilmFile>().Include(ff => ff.Film).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error: could not find the specified Entities");
            }
        }
    }
}
