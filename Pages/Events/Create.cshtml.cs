using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Final.Models.BindingModels;
using Final.Models;
using Final.Services;

namespace Final.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly IEventRepository _eventRepository;

        public CreateModel(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [BindProperty]
        public EventFormModel Event { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var newEvent = new Event
            {
                Title = Event.Title!,
                Description = Event.Description!,
                Date = Event.Date,
                Location = Event.Location!,
                Capacity = Event.Capacity,
                CreatedBy = User.Identity?.Name ?? "Anonim"
            };
            _eventRepository.Add(newEvent);
            TempData["Success"] = "Etkinlik başarıyla oluşturuldu!";
            return RedirectToPage("Index");
        }
    }
}