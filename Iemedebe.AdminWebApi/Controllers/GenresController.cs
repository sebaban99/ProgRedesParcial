using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.Domain;
using Iemedebe.CommonsWebApi;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.BusinessLogic;
using Iemedebe.CommonsWebApi;
using System.Web.Http;
using System.Web;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("genres")]
    [ApiController]
    public class GenresController : ApiController
    {
        private readonly ILogic<Genre> genreLogic;

        public GenresController(ILogic<Genre> genreLogic) : base()
        {
            this.genreLogic = genreLogic;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            await Task.Yield();
            try
            {
                var genres = await genreLogic.GetAllAsync().ConfigureAwait(false);
                IEnumerable<GenreDTO> genresReturned = genres.Select(u => new GenreDTO(u));
                return Ok(genresReturned);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostAsync([FromBody]GenreDTO model)
        {
            await Task.Yield();
            try
            {
                var genreToCreate = model.ToEntity();
                var genre = await genreLogic.CreateAsync(genreToCreate).ConfigureAwait(false);
                return Ok(new GenreDTO(genre));
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
                var genre = await genreLogic.GetAsync(id).ConfigureAwait(false);
                if (genre == null)
                {
                    return Ok("There is no genre with id: " + id);
                }
                return Ok(new GenreDTO(genre));
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
                var genreToDelete = await genreLogic.GetAsync(id).ConfigureAwait(false);
                await genreLogic.RemoveAsync(genreToDelete);
                return Ok("Genre successfully deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] GenreDTO model)
        {
            await Task.Yield();
            try
            {

                var genreToUpdate = await genreLogic.GetAsync(id).ConfigureAwait(false);
                var updatedGenre = model.ToEntity();
                await genreLogic.UpdateAsync(updatedGenre, genreToUpdate);
                return Ok("Genre successfully updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}