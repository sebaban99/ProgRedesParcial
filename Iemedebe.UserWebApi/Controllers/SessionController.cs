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
using System.Text;

namespace Iemedebe.UserWebApi.Controllers
{
    [Route("sessions")]
    [ApiController]
    public class SessionController : ApiController
    {

        private static string baseURI = "http://localhost:8080/api/sessions";
        private HttpClient httpClient = new HttpClient();


        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginDTO model)
        {
            var content = JsonConvert.SerializeObject(model);
            var httpResponse = await httpClient.PostAsync(baseURI, new StringContent(content, Encoding.Default, "application/json")).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            var session = JsonConvert.DeserializeObject<Guid>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return Ok(session.ToString());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> LogOut(Guid id)
        {
            var httpResponse = await httpClient.DeleteAsync($"{baseURI}{id}").ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            return Ok();

        }
    }
}