using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Iemedebe.CommonWebApi;
using System.Net.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using Newtonsoft.Json;
using Iemedebe.BusinessLogic;
using Iemedebe.Domain;
using System.Text;
using Iemedebe.UserWebApi.Filters;

namespace Iemedebe.UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private IFilmLogic<Film> filmLogic;
        private string baseURI = "https://localhost:5005/api/films";
        private HttpClient httpClient;

        public FilmsController(IFilmLogic<Film> filmLogic)
        {
            this.filmLogic = filmLogic;
            httpClient = new HttpClient();
        }

        [HttpGet()]
        [ProtectFilter()]
        public async Task<IActionResult> GetAllAsync()
        {
            await Task.Yield();
            try
            {
                var httpResponse = await httpClient.GetAsync(baseURI).ConfigureAwait(false);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return BadRequest(httpResponse.RequestMessage.ToString());
                }

                var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var films = JsonConvert.DeserializeObject<List<FilmDTO>>(content);

                return Ok(films);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/ratings")]
        [ProtectFilter()]
        public async Task<IActionResult> PostRatingAsync([FromBody]RatingDTO model)
        {
            var content = JsonConvert.SerializeObject(model);
            var httpResponse = await httpClient.PostAsync(baseURI + $"{model.RatedFilm.Id}/ratings", new StringContent(content, Encoding.Default, "application/json")).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            var film = JsonConvert.DeserializeObject<FilmDTO>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return Ok(film);
        }

        [HttpPost("{id}/ratings/{idRating}")]
        [ProtectFilter()]
        public async Task<IActionResult> PutRatingAsync(Guid id, Guid idRating, [FromBody]RatingDTO model)
        {
            var content = JsonConvert.SerializeObject(model);
            var httpResponse = await httpClient.PutAsync($"{baseURI}{id}/ratings/{idRating}", new StringContent(content, Encoding.Default, "application/json")).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            var updatedFilm = JsonConvert.DeserializeObject<FilmDTO>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return Ok(updatedFilm);
        }

        [HttpDelete("{id}/ratings/{idRating}")]
        [ProtectFilter()]
        public async Task<IActionResult> DeleteRatingAsync(Guid id, Guid idRating)
        {
            var httpResponse = await httpClient.DeleteAsync($"{baseURI}{id}/ratings/{idRating}").ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            return Ok();
        }
    }
}