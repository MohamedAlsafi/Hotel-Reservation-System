using Hotel_Reservation_System.Error;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Hotel_Reservation_System.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Middleware}] {ExceptionType}: {Message}", nameof(ExceptionMiddleware), ex.GetType().Name, ex.Message);

                httpContext.Response.StatusCode = ex switch
                {
                    InvalidOperationException or ArgumentException => (int)HttpStatusCode.BadRequest, // 400
                    KeyNotFoundException => (int)HttpStatusCode.NotFound, // 404
                    _ => (int)HttpStatusCode.InternalServerError // 500
                };

                httpContext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment()
                    ? new ApiExcaptionResponse(httpContext.Response.StatusCode, $"{ex.GetType().Name}: {ex.Message}", ex.StackTrace)
                    : new ApiExcaptionResponse(httpContext.Response.StatusCode, "An unexpected error occurred. Please try again later.");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
