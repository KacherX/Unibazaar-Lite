using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Final.Models;
using Final.Services;

namespace Final.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly IEventRepository _eventRepository;

        public EditModel(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [BindProperty]
        public Event Event { get; set; } = null!;

        public IActionResult OnGet(int id)
        {
            var ev = _eventRepository.GetById(id);
            if (ev == null)
                return NotFound();

            // Security: only creator can edit
            if (ev.CreatedBy != User.Identity?.Name)
                return Forbid();

            Event = ev;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var ev = _eventRepository.GetById(id);
            if (ev == null)
                return NotFound();

            if (ev.CreatedBy != User.Identity?.Name)
                return Forbid();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update the event properties
            ev.Title = Event.Title;
            ev.Description = Event.Description;
            ev.Date = Event.Date;
            ev.Location = Event.Location;
            ev.Capacity = Event.Capacity;

            _eventRepository.Update(ev); // Implement Update method

            TempData["Success"] = "Etkinlik başarıyla güncellendi.";
            return RedirectToPage("Index");
        }
    }
}