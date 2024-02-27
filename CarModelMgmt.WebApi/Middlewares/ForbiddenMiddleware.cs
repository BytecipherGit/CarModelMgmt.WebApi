using CarModelMgmt.Services.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CarModelMgmt.WebApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ForbiddenMiddleware
    {
        private readonly RequestDelegate _next;

        public ForbiddenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Response.StatusCode == 403)
            {
                var apiResponse = new ApiResponse<object>(false, null, "Access Forbidden", 403);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
            }
            else
            {
                await _next(context);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ForbiddenMiddlewareExtensions
    {
        public static IApplicationBuilder UseForbiddenMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ForbiddenMiddleware>();
        }
    }
}
