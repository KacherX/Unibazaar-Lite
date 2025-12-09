using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Final.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Username { get; set; } = "";

        [BindProperty]
        [Required]
        public string Password { get; set; } = "";

        [BindProperty]
        [Required]
        public string DisplayName { get; set; } = "";

        public void OnGet() { }

        public IActionResult OnPost()
        {
            Response.Cookies.Delete("FakeAuthUser");
            return RedirectToPage("/Index");
        }
    }
}