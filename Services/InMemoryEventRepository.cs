using System.Collections.Generic;
using System.Linq;
using Final.Models;

namespace Final.Services
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _events = new();
        private int _nextId = 1;

        public IEnumerable<Event> GetAll() => _events;

        public Event? GetById(int id) => _events.FirstOrDefault(e => e.Id == id);

        public void Add(Event ev)
        {
            ev.Id = _nextId++;
            _events.Add(ev);
        }

        public void Update(Event ev)
        {
            var existing = GetById(ev.Id);
            if (existing != null)
            {
                existing.Title = ev.Title;
                existing.Description = ev.Description;
                existing.Date = ev.Date;
                existing.Location = ev.Location;
                existing.Capacity = ev.Capacity;
            }
        }

        public void Delete(int id)
        {
            var ev = GetById(id);
            if (ev != null)
                _events.Remove(ev);
        }

        public bool Exists(int id) => _events.Any(e => e.Id == id);
    }
}