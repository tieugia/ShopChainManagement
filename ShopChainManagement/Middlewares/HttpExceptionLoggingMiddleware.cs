using Serilog;

namespace ShopChainManagement.Middlewares
{
    public class HttpExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Unhandled exception during HTTP request processing with message {ex.Message}");
            }
        }
    }

}
