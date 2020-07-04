using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.BusinessLogic;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using Iemedebe.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Iemedebe.AdminWebApi
{
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
        private ISessionLogic sessionLogic;

        public SessionController(ISessionLogic sessionLogic)
        {
            this.sessionLogic = sessionLogic;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            User loggedUser = await sessionLogic.ValidateLogin(model.Email, model.Password).ConfigureAwait(false);
            if (loggedUser == null)
            {
                return BadRequest("Login error: Incorrect email or password");
            }
            else
            {
                Session.LoggedUser = loggedUser;
                return Ok(loggedUser.Id);
            }
        }

        [AuthenticationFilter()]
        [HttpDelete("{id}")]
        public async Task<IActionResult> LogOut(Guid id)
        {
            try
            {
                await sessionLogic.DeleteSession(id).ConfigureAwait(false);
                return Ok();
            }
            catch(BusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}