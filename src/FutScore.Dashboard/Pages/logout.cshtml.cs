using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Çıkış işlemi
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Tüm session verilerini temizle
            //HttpContext.Session.Clear();

            // Kullanıcıyı login sayfasına yönlendir
            return RedirectToPage("/login");
        }
    }
} 