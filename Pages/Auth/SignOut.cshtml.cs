using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final.Pages.Auth
{
    public class SignOutModel : PageModel
    {
        public IActionResult OnPost()
        {
            // TempData.Remove("CurrentUser"); // Gerek yok, kullanıcıyı anonim yapacağız
            return RedirectToPage("/Index", new { user = "Anonim" });
        }
    }
}