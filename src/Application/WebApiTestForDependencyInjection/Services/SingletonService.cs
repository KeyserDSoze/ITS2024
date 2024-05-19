namespace WebApi.Services
{
    public sealed class SingletonService
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
