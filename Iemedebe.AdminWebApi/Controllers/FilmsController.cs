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
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;

namespace Iemedebe.AdminWebApi.Controllers
{
    [Route("films")]
    [ApiController]
    public class FilmsController : ApiController
    {
        private readonly ILogic<Film> filmLogic;

        public FilmsController(ILogic<Film> filmLogic) : base()
        {
            this.filmLogic = filmLogic;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            await Task.Yield();
            try
            {
                var films = await filmLogic.GetAllAsync().ConfigureAwait(false);
                IEnumerable<FilmDTO> filmsReturned = films.Select(u => new FilmDTO(u));
                return Ok(filmsReturned);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostAsync([FromBody]FilmDTO model)
        {
            await Task.Yield();
            try
            {
                var filmToCreate = model.ToEntity();
                var film = await filmLogic.CreateAsync(filmToCreate).ConfigureAwait(false);
                return Ok(new FilmDTO(film));
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
                var film = await filmLogic.GetAsync(id).ConfigureAwait(false);
                if (film == null)
                {
                    return Ok("There is no film with id: " + id);
                }
                return Ok(new FilmDTO(film));
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
                var filmToDelete = await filmLogic.GetAsync(id).ConfigureAwait(false);
                await filmLogic.RemoveAsync(filmToDelete);
                return Ok("Film successfully deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] FilmDTO model)
        {
            await Task.Yield();
            try
            {

                var filmToUpdate = await filmLogic.GetAsync(id).ConfigureAwait(false);
                var updatedFilm = model.ToEntity();
                await filmLogic.UpdateAsync(updatedFilm, filmToUpdate);
                return Ok("Film successfully updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}