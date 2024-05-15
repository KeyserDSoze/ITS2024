using Amazon.Payment.Domain;
using Amazon.Payment.Domain.Services;

namespace Amazon.Payment.Sql
{
    public sealed class CartStorageService : ICartStorageService
    {
        private Dictionary<Guid, List<Item>> _carts = new();
        public IEnumerable<Item> List(Guid cartId)
        {
            if (_carts.ContainsKey(cartId))
                return _carts[cartId];
            else
                return new List<Item>();
        }
        public bool AddItem(Item item, Guid cartId)
        {
            if (!_carts.ContainsKey(cartId))
                _carts.Add(cartId, new List<Item>());
            _carts[cartId].Add(item);
            return true;
        }
        public bool Delete(Guid itemId, Guid cartId)
        {
            if (_carts.ContainsKey(cartId))
            {
                var item = _carts[cartId].FirstOrDefault(i => i.Id == itemId);
                if (item != default)
                {
                    _carts[cartId].Remove(item);
                    return true;
                }
            }
            return false;
        }
    }
}
