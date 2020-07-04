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
    [Route("directors")]
    [ApiController]
    public class DirectorsController : ApiController
    {
        private readonly ILogic<Director> directorLogic;

        public DirectorsController(ILogic<Director> directorLogic) : base()
        {
            this.directorLogic = directorLogic;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
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
        public async Task<IHttpActionResult> PostAsync([FromBody]DirectorDTO model)
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
        public async Task<IHttpActionResult> GetAsync(Guid id)
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
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
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
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] DirectorDTO model)
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