using System.Collections.Generic;
using Final.Models;

namespace Final.Services
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        Event? GetById(int id);
        void Add(Event ev);
        void Update(Event ev);
        void Delete(int id);
        bool Exists(int id);
    }
}