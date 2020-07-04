using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.Domain;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.BusinessLogic;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogic<User> userLogic;

        public UsersController(ILogic<User> userLogic) : base()
        {
            this.userLogic = userLogic;
        }

        [HttpGet]
        [Route("")]
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

        [HttpPost]
        [Route("")]
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

        [HttpGet]
        [Route("{nickname}")]
        public async Task<IActionResult> GetAsync(string nickname)
        {
            await Task.Yield();
            try
            {
                var user = await userLogic.GetByConditionAsync(u => u.Nickname == nickname).ConfigureAwait(false);
                if (user == null)
                {
                    return Ok("There is no user with the nickname: " + nickname);
                }
                return Ok(new UserDTO(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{nickname}")]
        public async Task<IActionResult> DeleteAsync(string nickname)
        {
            await Task.Yield();
            try
            {
                var userToDelete = await userLogic.GetByConditionAsync(u => u.Nickname == nickname).ConfigureAwait(false);
                await userLogic.RemoveAsync(userToDelete);
                return Ok("User successfully deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{nickname}")]
        public async Task<IActionResult> PutAsync(string nickname, [FromBody] UserDTO model)
        {
            await Task.Yield();
            try
            {
             
                var userToUpdate = await userLogic.GetByConditionAsync(u => u.Nickname == nickname).ConfigureAwait(false);
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