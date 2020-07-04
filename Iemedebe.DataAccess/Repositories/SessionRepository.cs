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
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(DbContext context)
        {
            Context = context;
        }

        public override async Task<Session> GetAsync(Guid id)
        {
            try
            {
                return await Context.Set<Session>().FirstAsync(x => x.Id == id).ConfigureAwait(false);
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

        public async Task<bool> ValidateSession(Guid token)
        {
            try
            {
                Session session = await GetByConditionAsync(q => q.UserId == token).ConfigureAwait(false);

                return session != null;
            }
            catch (Exception e)
            {
                throw (new DataAccessException("Error, the session is not valid " + e));
            }
        }
    }
}
