using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using Iemedebe.Exceptions;

namespace Iemedebe.BusinessLogic
{
    public class SessionLogic : ISessionLogic<Session>
    {
        private IRepository<User> userRepository;
        private ISessionRepository sessionRepository;

        public SessionLogic(IRepository<User> adminRepository,
        ISessionRepository sessionRepository)
        {
            this.userRepository = adminRepository;
            this.sessionRepository = sessionRepository;
        }

        public async Task DeleteSession(Guid token)
        {
            if(token == null || !await ValidateSession(token).ConfigureAwait(false))
            {
                throw new BusinessLogicException("Error: Invalid session token, there is no session corresponding to this token");
            }
            else
            {
                Session sessionToRemove = await sessionRepository.GetByConditionAsync(s => s.UserId == token).ConfigureAwait(false);
                sessionRepository.Remove(sessionToRemove);
                await sessionRepository.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task GenerateSession(User loggedUser)
        {
            Session s = new Session()
            {
                Id = Guid.NewGuid(),
                UserId = loggedUser.Id
            };
            sessionRepository.Add(s);
            await sessionRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Session> GetAsync(Guid token)
        {
            return await sessionRepository.GetAsync(token).ConfigureAwait(false);
        }

        public async Task<User> ValidateLogin(string email, string password)
        {
            if (email != null && password != null)
            {
                User loggedUser = await userRepository.GetByConditionAsync(a => a.Email == email).ConfigureAwait(false);
                if (loggedUser != null && loggedUser.Password.Equals(password))
                {
                    if (!await ValidateSession(loggedUser.Id).ConfigureAwait(false))
                    {
                        await GenerateSession(loggedUser).ConfigureAwait(false);
                    }
                    return loggedUser;
                }
            }
            return null;
        }

        public async Task<bool> ValidateSession(Guid token)
        {
            return await sessionRepository.ValidateSession(token).ConfigureAwait(false);
        }
    }
}
