using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmartSolutionCRM.Infrastructure.ErrorHandling;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace SmartSolutionCRM.Infrastructure.Middlewares
{
    internal class AuthorizationErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            var statusCode = context.Response.StatusCode;
            if ((statusCode == 403 || statusCode == 401) && !context.Response.HasStarted)
            {
                var exceptionName = statusCode == 401 ? nameof(AuthenticationException) : nameof(UnauthorizedAccessException);
                var message = statusCode == 401 ? "Unauthorized" : "Forbidden";
                var response =
                    JsonConvert.SerializeObject(
                        new JsonErrorResponse(exceptionName, message, null), new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            Formatting = Formatting.Indented
                        });
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(response);
            }
        }
    }
}
