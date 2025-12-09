using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Final.Models;
using Final.Services;

namespace Final.Pages.Events
{
    public class RegisterModel : PageModel
    {
        private readonly IEventRepository _eventRepository;

        public RegisterModel(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Event? Event { get; set; }

        public IActionResult OnGet(int id)
        {
            Event = _eventRepository.GetById(id);
            if (Event == null)
                return NotFound();
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var ev = _eventRepository.GetById(id);
            if (ev == null)
                return NotFound();

            if (ev.Capacity > 0)
            {
                ev.Capacity--; // Reduce kontenjan
                TempData["Success"] = "Etkinliğe başarıyla kayıt oldunuz!";
            }
            else
            {
                TempData["Error"] = "Etkinlik kontenjanı dolu! Kayıt yapılamadı.";
            }

            return RedirectToPage("/Events/Index");
        }
    }
}