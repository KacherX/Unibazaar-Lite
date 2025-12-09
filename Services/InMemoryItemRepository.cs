using System.Collections.Generic;
using System.Linq;
using Final.Models;

namespace Final.Services
{
    public class InMemoryItemRepository : IItemRepository
    {
        private readonly List<Item> _items = new();
        private int _nextId = 1;

        public IEnumerable<Item> GetAll() => _items;

        public Item? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);

        public void Add(Item item)
        {
            item.Id = _nextId++;
            item.PostedAt = System.DateTime.Now;
            _items.Add(item);
        }

        public void Update(Item item)
        {
            var existing = GetById(item.Id);
            if (existing != null)
            {
                existing.Title = item.Title;
                existing.Description = item.Description;
                existing.Price = item.Price;
                existing.Seller = item.Seller;
                // PostedAt değişmez
            }
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
                _items.Remove(item);
        }

        public bool Exists(int id) => _items.Any(i => i.Id == id);
    }
}