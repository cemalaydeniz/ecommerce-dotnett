using ecommerce_dotnet.Utility;
using System.Net;
using System.Text.Json;

namespace ecommerce_dotnet.Middlewares
{
    public class InternalErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<InternalErrorHandler> _logger;

        public InternalErrorHandler(RequestDelegate next, ILogger<InternalErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError("Internal Server Error in {0}\nError: {1}", context.Request.Path, e.ToString());

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Request.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(JsonResponse.Error(Constants.Response.General.InternalServerError)));
            }
        }
    }
}
