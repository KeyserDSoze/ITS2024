using Amazon.Payment.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Payment.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]/[action]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item> List(Guid cartId)
        {
            return Array.Empty<Item>();
        }
        [HttpPost]
        public bool AddItem(/*add item*/)
        {
            return true;
        }
        [HttpDelete]
        public bool Delete(int itemId, Guid cartId)
        {
            // Delete item
            return true;
        }
    }
}
