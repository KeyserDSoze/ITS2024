using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IManager _manager;

        public ExampleController(
            ILogger<ExampleController> logger,
            IManager manager)
        {
            _logger = logger;
            _manager = manager;
        }
        [HttpGet]
        public IEnumerable<string> Get(CancellationToken cancellationToken)
        {
            var type = _manager.GetType();
            return _manager.Get(cancellationToken);
        }
    }
}
