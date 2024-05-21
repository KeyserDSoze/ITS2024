using Amazon.Payment.Domain;
using Amazon.Payment.Domain.Services;

namespace Amazon.Payment.Sql
{
    public sealed class CartStorageServiceWithSql : ICartStorageService
    {
        private readonly PaymentDbContext _context;
        public CartStorageServiceWithSql(PaymentDbContext context)
        {
            _context = context;
        }
        public bool AddItem(Item item, Guid cartId)
        {
            item.CartId = cartId;
            _context.Items.Add(item);
            return _context.SaveChanges() == 1;
        }
        public bool Delete(Guid itemId, Guid cartId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Item> List(Guid cartId)
        {
           var items = _context.Items
                .Where(x => x.CartId == cartId && x.IsAvailable);
            return items.ToList();
        }
    }
}
