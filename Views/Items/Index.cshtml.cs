using Microsoft.AspNetCore.Mvc.RazorPages;
using Final.Models;
using Final.Services;
using System.Collections.Generic;

namespace Final.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly IItemRepository _itemRepository;

        public IndexModel(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public List<Item> Items { get; set; } = new();

        public void OnGet()
        {
            Items = _itemRepository.GetAll().ToList();
        }
    }
}