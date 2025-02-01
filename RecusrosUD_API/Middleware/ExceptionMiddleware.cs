using Newtonsoft.Json;
using System.Net;

namespace RecusrosUD_API.Middleware
{

    public class ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly IWebHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = $"Error: {exception.Message}",
                StackTrace = _env.IsDevelopment() ? exception.StackTrace ?? "" : ""
            }));
        }
    }



}
