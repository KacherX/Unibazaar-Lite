using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Final.Models.BindingModels;
using Final.Models;
using Final.Services;

namespace Final.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly IItemRepository _itemRepository;

        public CreateModel(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [BindProperty]
        public ItemFormModel Item { get; set; } = new();

        [BindProperty]
        public IFormFile? Photo { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var newItem = new Item
            {
                Title = Item.Title!,
                Description = Item.Description!,
                Price = Item.Price,
                Seller = User.Identity?.Name ?? "Anonim",
                PostedAt = DateTime.Now
            };
            if (Photo != null && Photo.Length > 0)
            {
                var fileName = Path.GetFileName(Photo.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(stream);
                }
                // İlan modeline fotoğraf yolunu ekle
                newItem.PhotoPath = "/uploads/" + fileName;
            }
            _itemRepository.Add(newItem);
            TempData["Success"] = "İlan başarıyla oluşturuldu!";
            return RedirectToPage("./Index");
        }
    }
}