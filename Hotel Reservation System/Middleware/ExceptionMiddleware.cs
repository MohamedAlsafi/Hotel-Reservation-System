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

        public ExceptionMiddleware(RequestDelegate next ,ILogger<ExceptionMiddleware> logger ,IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext); //go to the next middelware

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message); //devolpment enviroment 

                httpcontext.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                httpcontext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ?
                    new ApiExcaptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExcaptionResponse((int)HttpStatusCode.InternalServerError);

                var json =JsonSerializer.Serialize(response);

                await httpcontext.Response.WriteAsync(json);


            }

        }






    }
}
