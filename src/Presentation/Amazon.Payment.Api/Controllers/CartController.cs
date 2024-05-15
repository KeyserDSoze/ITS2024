using Amazon.Payment.Domain;
using Amazon.Payment.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Payment.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]/[action]")]
    public class CartController : ControllerBase
    {
        private readonly ICartStorageService _cartStorageService;
        private readonly ILogger<CartController> _logger;

        public CartController(
            ICartStorageService cartStorageService,
            ILogger<CartController> logger)
        {
            _cartStorageService = cartStorageService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item> List(Guid cartId)
        {
            return _cartStorageService.List(cartId);
        }
        [HttpPost]
        public bool AddItem(Item item, [FromQuery] Guid cartId)
        {
            return _cartStorageService.AddItem(item, cartId);
        }
        [HttpDelete]
        public bool Delete(Guid itemId, Guid cartId)
        {
            return _cartStorageService.Delete(itemId, cartId);
        }
    }
}
