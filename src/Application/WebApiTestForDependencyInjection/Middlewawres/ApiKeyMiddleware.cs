
namespace WebApi.Middlewawres
{
    public class ApiKeyMiddleware : IMiddleware
    {
        private readonly string _apiKey;
        public ApiKeyMiddleware(IConfiguration configuration) 
        {
            _apiKey = configuration["ApiKey"]!;
        }
        public Task InvokeAsync(HttpContext context,
            RequestDelegate next)
        {
            context.Request.Query.TryGetValue("apiKey", out var apiKey);
            if (apiKey == _apiKey)
            {
                return next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            }
        }
    }
}
