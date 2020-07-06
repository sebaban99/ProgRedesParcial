using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.BusinessLogic;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using Iemedebe.CommonWebApi;
using Iemedebe.Exceptions;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace Iemedebe.AdminWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private ISessionLogic<Session> sessionLogic;

        public SessionsController(ISessionLogic<Session> sessionLogic)
        {
            this.sessionLogic = sessionLogic;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            await Task.Yield();

            var session = await sessionLogic.GetAsync(id).ConfigureAwait(false);
            if (session == null)
            {
                return BadRequest("Login error: Incorrect email or password");
            }
            else
            {
                return Ok(new SessionDTO(session));
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            await Task.Yield();

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
            await Task.Yield();

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