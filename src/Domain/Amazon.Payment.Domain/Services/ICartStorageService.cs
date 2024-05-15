using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Payment.Domain.Services
{
    public interface ICartStorageService
    {
        IEnumerable<Item> List(Guid cartId);
        bool AddItem(Item item, Guid cartId);
        bool Delete(Guid itemId, Guid cartId);
    }
}
