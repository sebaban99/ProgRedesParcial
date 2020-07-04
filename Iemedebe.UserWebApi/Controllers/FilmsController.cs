using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Iemedebe.CommonWebApi;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using System.Net.Http;
using Newtonsoft.Json;
using Iemedebe.BusinessLogic;
using Iemedebe.Domain;
using System.Text;

namespace Iemedebe.UserWebApi.Controllers
{
    [Route("films")]
    [ApiController]
    public class FilmsController : ApiController
    {
        private IFilmLogic<Film> filmLogic;
        private string baseURI = "http://localhost:8080/api/films";
        private HttpClient httpClient;

        public FilmsController(IFilmLogic<Film> filmLogic)
        {
            this.filmLogic = filmLogic;
            httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
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

        [HttpPost]
        [Route("{id}/ratings")]
        public async Task<IHttpActionResult> PostRatingAsync([FromBody]RatingDTO model)
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

        [HttpPost]
        [Route("{id}/ratings/{idRating}")]
        public async Task<IHttpActionResult> PutRatingAsync(Guid id, Guid idRating, [FromBody]RatingDTO model)
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

        [HttpDelete]
        [Route("{id}/ratings/{idRating}")]
        public async Task<IHttpActionResult> DeleteRatingAsync(Guid id, Guid idRating)
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