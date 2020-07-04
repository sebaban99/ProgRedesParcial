using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.BusinessLogic;
using Iemedebe.DataAccess;
using Iemedebe.Domain;
using Iemedebe.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Web;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using Microsoft.AspNetCore.Components;

namespace Iemedebe.AdminWebApi
{
    [Route("sessions")]
    public class SessionsController : ApiController
    {
        private ISessionLogic sessionLogic;

        public SessionsController(ISessionLogic sessionLogic)
        {
            this.sessionLogic = sessionLogic;
        }
        
        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginDTO model)
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
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> LogOut(Guid id)
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