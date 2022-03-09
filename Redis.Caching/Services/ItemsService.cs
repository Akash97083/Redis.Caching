using Redis.Caching.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redis.Caching.Services
{
    public class ItemsService : IItemsService
    {

        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Portion", Price = 9 },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20},
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18}
        };


        public Task CreateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }
    }
}
