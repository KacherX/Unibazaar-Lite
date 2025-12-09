using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Final.Pages.Auth
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Username { get; set; } = "";

        [BindProperty]
        [Required]
        public string Password { get; set; } = "";

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Cookie'ye kullanıcıyı yaz
            Response.Cookies.Append("FakeAuthUser", Username, new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            TempData["Success"] = "Başarıyla giriş yaptınız!";
            return RedirectToPage("/Events/Index");
        }
    }
}