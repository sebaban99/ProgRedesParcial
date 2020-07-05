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
    public class FavouritesController : ControllerBase
    {
        private IUserLogic<User> userLogic;
        private string baseURI = "https://localhost:5005/api/users";
        private HttpClient httpClient;

        public FavouritesController(IUserLogic<User> userLogic)
        {
            this.userLogic = userLogic;
            httpClient = new HttpClient();
        }

        [HttpPost("{id}")]
        [ProtectFilter()]
        public async Task<IActionResult> PostAsync([FromBody]FavouriteDTO model)
        {
            var content = JsonConvert.SerializeObject(model);
            var httpResponse = await httpClient.PostAsync(baseURI + $"{model.UserId}/favourites", new StringContent(content, Encoding.Default, "application/json")).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            var user = JsonConvert.DeserializeObject<UserDTO>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [ProtectFilter()]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var loggedUserId = "";
            var httpResponse = await httpClient.DeleteAsync($"{baseURI}/{loggedUserId}/favourites/{id}").ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            return Ok();
        }
    }
}