using System;

namespace Redis.Caching.Models
{
    public class Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
    }
}
