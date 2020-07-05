using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iemedebe.Domain;
using Iemedebe.CommonWebApi;
using Microsoft.AspNetCore.Mvc;
using Iemedebe.BusinessLogic;
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
    public class GenresController : ControllerBase
    {
        private readonly ILogic<Genre> genreLogic;

        public GenresController(ILogic<Genre> genreLogic) : base()
        {
            this.genreLogic = genreLogic;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
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

        [HttpPost()]
        public async Task<IActionResult> PostAsync([FromBody]GenreDTO model)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] GenreDTO model)
        {
            await Task.Yield();
            try
            {

                var originalGenre = await genreLogic.GetAsync(id).ConfigureAwait(false);
                var updatedGenre = model.ToEntity();
                var modifiedEntity = await genreLogic.UpdateAsync(updatedGenre, originalGenre);
                return Ok(new GenreDTO(modifiedEntity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}