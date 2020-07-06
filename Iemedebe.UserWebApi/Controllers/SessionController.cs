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
using System.Text;

namespace Iemedebe.UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        private static string baseURI = "https://localhost:5005/api/sessions";
        private HttpClient httpClient = new HttpClient();


        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> LogOut(Guid id)
        {
            var httpResponse = await httpClient.DeleteAsync($"{baseURI}/{id}").ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest(httpResponse.RequestMessage.ToString());
            }

            return Ok();

        }
    }
}