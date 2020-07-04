using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Iemedebe.BusinessLogic;

namespace Iemedebe.AdminWebApi
{
    public class AuthenticationFilter : Attribute, IAsyncAuthorizationFilter
    {
        private ISessionLogic GetSessionLogic(AuthorizationFilterContext context)
        {
            var typeOfSessionsLogic = typeof(ISessionLogic);
            return context.HttpContext.RequestServices.GetService(typeOfSessionsLogic) as ISessionLogic;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["token"];
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Invalid session token"
                };
                return;
            }

            var sessionLogic = GetSessionLogic(context);
            
            if (!await sessionLogic.ValidateSession(new Guid(token)).ConfigureAwait(false))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "AFValueItemDTO valid session is needed"
                };
                return;
            }
        }
    }
}