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
        private readonly IUserLogic<User> userLogic;
        public UsersController(IUserLogic<User> userLogic) : base()
        {
            this.userLogic = userLogic;
        }


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

        //Add Filter
        [HttpPost("{id}/favourites")]
        public async Task<IActionResult> PostFavouriteAsync([FromBody]FavouriteDTO favouriteDTO)
        {
            await Task.Yield();
            try
            {
                var favourite = favouriteDTO.ToEntity();
                var user = await userLogic.AddFavouriteAsync(favourite).ConfigureAwait(false);
                return Ok(new UserDTO(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AuthenticationFilter()]
        [HttpPost("{id}/favourites/{idFavourite}")]
        public async Task<IActionResult> DeleteFavouriteAsync(Guid id, Guid idFavourite)
        {
            await Task.Yield();
            try
            {
                await userLogic.RemoveFavouriteAsync(id, idFavourite).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}