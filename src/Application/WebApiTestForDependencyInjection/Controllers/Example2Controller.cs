using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Example2Controller : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IManager _manager;
        private readonly IManager _manager2;

        public Example2Controller(
            ILogger<ExampleController> logger,
            IManager manager,
            IManager manager2)
        {
            _logger = logger;
            _manager = manager;
            _manager2 = manager2;
        }
        [HttpGet]
        public IEnumerable<string> Get(CancellationToken cancellationToken)
        {
            var listOfItems = _manager.Get(cancellationToken).ToList();
            listOfItems.AddRange(_manager2.Get(cancellationToken));
            return listOfItems;
        }
    }
}
