namespace WebApi.Services
{
    public sealed class TransientService
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
