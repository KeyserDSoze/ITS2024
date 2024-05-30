using Amazon.Authentication.Domain;

namespace Amazon.Payment.Api.Middlewares
{
    public class ApiKeyMiddleware : IMiddleware
    {
        private readonly IApiKeyHandler _apiKey;
        public ApiKeyMiddleware(IApiKeyHandler apiKey)
        {
            _apiKey = apiKey;
        }
        public Task InvokeAsync(HttpContext context,
            RequestDelegate next)
        {
            context.Request.Query.TryGetValue("apiKey", out var apiKey);
            if (apiKey == _apiKey.GetValue())
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
