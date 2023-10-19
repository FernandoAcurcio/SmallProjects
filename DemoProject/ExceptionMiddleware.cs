using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoProject
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next { get; }

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (InvalidNameException)
            {
                httpContext.Response.ContentType = "application/problem+json";
                httpContext.Response.StatusCode = 400;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = string.Empty,
                    Title = "Name is invalid",
                    Type = "Error"
                };

                var problemDetaisJson = JsonSerializer.Serialize(problemDetails);
                await httpContext.Response.WriteAsync(problemDetaisJson);
            }
            catch (Exception)
            {
                httpContext.Response.ContentType = "application/problem+json";
                httpContext.Response.StatusCode = 500;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = string.Empty,
                    Instance = string.Empty,
                    Title = "Internal Server Error - something went wrong",
                    Type = "Error"
                };

                var problemDetaisJson = JsonSerializer.Serialize(problemDetails);
                await httpContext.Response.WriteAsync(problemDetaisJson);
            }
        }
    }
}
