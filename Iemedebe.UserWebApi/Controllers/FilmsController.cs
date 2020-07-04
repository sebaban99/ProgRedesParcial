using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Iemedebe.CommonsWebApi;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using System.Net.Http;

namespace Iemedebe.UserWebApi.Controllers
{
    [Route("films")]
    [ApiController]
    public class FilmsController : ApiController
    {
        private string baseURI = "http://localhost:8080/sessions";

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(baseURI);
                var response = client.GetAsync(uri).Result;
                var responseMessage = response.RequestMessage.ToString();

                if (response.IsSuccessStatusCode)
                {
                    return Ok(responseMessage);
                }

                return BadRequest(responseMessage);
            }
        }
    }
}