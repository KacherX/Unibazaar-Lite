using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Final.Models;
using Final.Services;

namespace Final.Pages.Events
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IEventRepository _eventRepository;

        public IndexModel(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public List<Event> Events { get; set; } = new();

        public void OnGet()
        {
            Events = _eventRepository.GetAll().OrderBy(e => e.Date).ToList();
        }
    }
}