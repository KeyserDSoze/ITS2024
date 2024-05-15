using Amazon.Payment.Domain;
using Amazon.Payment.Domain.Services;

namespace Amazon.Payment.Sql
{
    public sealed class CartStorageService2 : ICartStorageService
    {
        public bool AddItem(Item item, Guid cartId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid itemId, Guid cartId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> List(Guid cartId)
        {
            throw new NotImplementedException();
        }
    }
}
