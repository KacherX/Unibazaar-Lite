using System.Collections.Generic;
using Final.Models;

namespace Final.Services
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item? GetById(int id);
        void Add(Item item);
        void Update(Item item);
        void Delete(int id);
        bool Exists(int id);
    }
}