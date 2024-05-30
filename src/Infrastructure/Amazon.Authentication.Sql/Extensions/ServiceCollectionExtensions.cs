using Amazon.Authentication.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amazon.Authentication.Sql.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSqlServer<AuthenticationDbContext>(configuration.GetConnectionString("Default"));
            services.AddScoped<IApiKeyHandler, ApiKeyHandler>();
            return services;
        }
    }
}
