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
using Microsoft.AspNetCore.Mvc.Filters;

namespace Iemedebe.UserWebApi.Filters
{
    public class ProtectFilter : Attribute, IAsyncAuthorizationFilter
    {
        private static string baseURI = "https://localhost:5005/api/sessions";
        private HttpClient httpClient = new HttpClient();

        public ProtectFilter()
        {
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["token"];
            var httpResponse = await httpClient.GetAsync($"{baseURI}/{token}").ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = "Authorization token is required",
                };
                return;

            }
        }
    }
}