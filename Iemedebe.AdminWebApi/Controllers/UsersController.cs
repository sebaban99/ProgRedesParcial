using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.Domain;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.BusinessLogic;
using System.Web.Http;
using System.Web;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ApiController
    {
        private readonly ILogic<User> userLogic;
        public UsersController(ILogic<User> userLogic) : base()
        {
            this.userLogic = userLogic;
        }


        // GET: /users
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            await Task.Yield();
            try
            {
                var users = await userLogic.GetAllAsync().ConfigureAwait(false);
                IEnumerable<UserDTO> usersReturned = users.Select(u => new UserDTO(u));
                return Ok(usersReturned);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: /users
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostAsync([FromBody]UserDTO model)
        {
            await Task.Yield();
            try
            {
                var userToCreate = model.ToEntity();
                var user = await userLogic.CreateAsync(userToCreate).ConfigureAwait(false);
                var userModel = new UserDTO(user);
                return Ok(userModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: /users/{nickname}
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            await Task.Yield();
            try
            {
                var user = await userLogic.GetAsync(id).ConfigureAwait(false);
                if (user == null)
                {
                    return Ok("There is no user with the id: " + id);
                }
                return Ok(new UserDTO(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: /users/{nickname}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await Task.Yield();
            try
            {
                var userToDelete = await userLogic.GetAsync(id).ConfigureAwait(false);
                await userLogic.RemoveAsync(userToDelete);
                return Ok("User successfully deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: /users/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] UserDTO model)
        {
            await Task.Yield();
            try
            {
             
                var userToUpdate = await userLogic.GetAsync(id).ConfigureAwait(false);
                var updatedUser = model.ToEntity();
                await userLogic.UpdateAsync(updatedUser, userToUpdate);
                return Ok("User successfully updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}