using Amazon.Payment.Domain;

namespace Amazon.Payment.Sql
{
    public sealed class CartStorageService
    {
        public IEnumerable<Item> List(Guid cartId)
        {
            //select * from items where id={cartId}
            return Array.Empty<Item>();
        }
        public bool AddItem(Item item)
        {
            //insert
            return true;
        }
        public bool Delete(int itemId, Guid cartId)
        {
            // Delete item
            return true;
        }
    }
}
