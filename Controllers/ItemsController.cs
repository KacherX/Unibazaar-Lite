using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Final.Services;
using Final.Models;
using Final.Models.BindingModels;
using Final.Filters;

namespace Final.Controllers
{
    [Route("items")]
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // /items
        [HttpGet("")]
        public IActionResult Index()
        {
            var items = _itemRepository.GetAll();
            return View(items);
        }

        // /items/details/{id}
        [HttpGet("details/{id}")]
        [ServiceFilter(typeof(ValidateItemExistsFilter))]
        public IActionResult Details(int id)
        {
            var item = _itemRepository.GetById(id);
            return View(item);
        }

        // /items/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            var model = new ItemFormModel();
            return View(model);
        }

        [HttpPost("create")]
        public IActionResult Create(ItemFormModel model, IFormFile? Photo)
        {
            
            Console.WriteLine($"[POST] Title: {model.Title}, Price: {model.Price}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[POST] ModelState is invalid!");
                return View(model);
            }
                

            string? photoPath = null;
            if (Photo != null && Photo.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(Photo.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }
                photoPath = "/uploads/" + fileName;
            }

            var item = new Item
            {
                Title = model.Title!,
                Description = model.Description!,
                Price = model.Price,
                Seller = User.Identity?.Name ?? "Anonymous",
                PostedAt = DateTime.Now,
                PhotoPath = photoPath
            };
            _itemRepository.Add(item);
            Console.WriteLine("[POST] Item added.");
            return RedirectToAction(nameof(Index));
        }

        // /api/items (JSON endpoint)
        [HttpGet("/api/items")]
        public IActionResult GetAllApi()
        {
            var items = _itemRepository.GetAll();
            return Json(items);
        }
    }
}