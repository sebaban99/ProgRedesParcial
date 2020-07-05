using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.Domain;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.BusinessLogic;
using Iemedebe.CommonWebApi;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogic<User> userLogic;
        public UsersController(ILogic<User> userLogic) : base()
        {
            this.userLogic = userLogic;
        }


        // GET: /users
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
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
        [HttpPost()]
        public async Task<IActionResult> PostAsync([FromBody]UserDTO model)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UserDTO model)
        {
            await Task.Yield();
            try
            {
             
                var userToUpdate = await userLogic.GetAsync(id).ConfigureAwait(false);
                var updatedUser = model.ToEntity();
                var updatedEntity = await userLogic.UpdateAsync(updatedUser, userToUpdate);
                return Ok(new UserDTO(updatedEntity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}