using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.Domain;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.BusinessLogic;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("directors")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly ILogic<Director> directorLogic;

        public DirectorsController(ILogic<Director> directorLogic) : base()
        {
            this.directorLogic = directorLogic;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            await Task.Yield();
            try
            {
                var directors = await directorLogic.GetAllAsync().ConfigureAwait(false);
                IEnumerable<DirectorDTO> directorsReturned = directors.Select(u => new DirectorDTO(u));
                return Ok(directorsReturned);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostAsync([FromBody]DirectorDTO model)
        {
            await Task.Yield();
            try
            {
                var directorToCreate = model.ToEntity();
                var director = await directorLogic.CreateAsync(directorToCreate).ConfigureAwait(false);
                return Ok(new DirectorDTO(director));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            await Task.Yield();
            try
            {
                var director = await directorLogic.GetAsync(id).ConfigureAwait(false);
                if (director == null)
                {
                    return Ok("There is no director with id: " + id);
                }
                return Ok(new DirectorDTO(director));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Task.Yield();
            try
            {
                var directorToDelete = await directorLogic.GetAsync(id).ConfigureAwait(false);
                await directorLogic.RemoveAsync(directorToDelete);
                return Ok("Director successfully deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] DirectorDTO model)
        {
            await Task.Yield();
            try
            {

                var directorToUpdate = await directorLogic.GetAsync(id).ConfigureAwait(false);
                var updatedDirector = model.ToEntity();
                await directorLogic.UpdateAsync(updatedDirector, directorToUpdate);
                return Ok("Director successfully updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}