using System.Text.Json;
using HairSalon.Core.Base;

namespace HairSalonBE.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, 
        IHostEnvironment env)
    { 
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var resopnse = env.IsDevelopment() ? new BaseException.ErrorException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
                    new BaseException.ErrorException(context.Response.StatusCode, ex.Message, "Internal Server Error");
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(resopnse, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
