using Amazon.Authentication.Domain;

namespace Amazon.Authentication.Sql.Services
{
    internal sealed class ApiKeyHandler : IApiKeyHandler
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public ApiKeyHandler(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }
        public string GetValue()
        {
            var apiKey = _authenticationDbContext.ApiKeys.FirstOrDefault();
            if (apiKey == null)
            {
                var randomApiKey = Guid.NewGuid().ToString();
                _authenticationDbContext.ApiKeys.Add(new Models.ApiKeyTable
                {
                    Value = randomApiKey
                });
                _authenticationDbContext.SaveChanges();
                return randomApiKey;
            }
            return apiKey!.Value;
        }
    }
}