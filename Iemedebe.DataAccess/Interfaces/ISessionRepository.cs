using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.DataAccess
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<bool> ValidateSession(Guid token);
    }
}
