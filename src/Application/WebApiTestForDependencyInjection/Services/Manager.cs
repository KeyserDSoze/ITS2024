namespace WebApi.Services
{
    public interface IManager
    {
        IEnumerable<string> Get(CancellationToken cancellationToken);
    }
    internal sealed class Manager2 : IManager
    {
        public IEnumerable<string> Get(CancellationToken cancellationToken)
        {
            yield return "da implementare";
        }
    }
    internal sealed class Manager : IManager
    {
        public SingletonService SingletonService { get; }
        public ScopedService ScopedService { get; }
        public ScopedService ScopedService2 { get; }
        public TransientService TransientService { get; }
        public TransientService TransientService2 { get; }
        public Manager(
            SingletonService singletonService,
            ScopedService scopedService,
            ScopedService scopedService2,
            TransientService transientService,
            TransientService transientService2)
        {
            SingletonService = singletonService;
            ScopedService = scopedService;
            ScopedService2 = scopedService2;
            TransientService = transientService;
            TransientService2 = transientService2;
        }
        public IEnumerable<string> Get(CancellationToken cancellationToken)
        {
            yield return $"Singleton: {SingletonService.Id}";
            yield return $"Scoped: {ScopedService.Id}";
            yield return $"Scoped2: {ScopedService2.Id}";
            if (!cancellationToken.IsCancellationRequested)
            {
                yield return $"Transient: {TransientService.Id}";
                yield return $"Transient2: {TransientService2.Id}";
            }
        }
    }
}
