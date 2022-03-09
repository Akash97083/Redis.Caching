using Redis.Caching.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redis.Caching.Services
{
    public interface IItemsService
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
    }
}
