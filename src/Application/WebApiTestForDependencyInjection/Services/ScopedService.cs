using WebApi.Controllers;

namespace WebApi.Services
{
    public sealed class ScopedService
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
