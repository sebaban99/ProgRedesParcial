using Iemedebe.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iemedebe.BusinessLogic
{
    public interface ISessionLogic
    {
        Task<User> ValidateLogin(string email, string password);

        Task<bool> ValidateSession(Guid token);

        Task GenerateSession(User loggedUser);

        Task DeleteSession(Guid token);
    }
}
