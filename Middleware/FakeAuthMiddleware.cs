using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Final.Services;

namespace Final.Middleware
{
    public class FakeAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public FakeAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Öncelik: Query string'den kullanıcıyı al
            var username = context.Request.Query["user"].ToString();

            // Eğer query string yoksa cookie'den oku
            if (string.IsNullOrEmpty(username))
                username = context.Request.Cookies["FakeAuthUser"];

            var user = FakeUserStore.Users.Find(u => u.Username == username);
            if (user == null)
            {
                // Anonim kullanıcı
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Anonim")
                };
                var identity = new ClaimsIdentity(claims, "FakeAuth");
                context.User = new ClaimsPrincipal(identity);
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("DisplayName", user.DisplayName)
                };
                var identity = new ClaimsIdentity(claims, "FakeAuth");
                context.User = new ClaimsPrincipal(identity);

                // Cookie'ye kullanıcıyı yaz (her istekte güncel tut)
                context.Response.Cookies.Append("FakeAuthUser", user.Username, new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
            }

            if (context.User.Identity?.Name == "Anonim" &&
                (context.Request.Path.StartsWithSegments("/Events") || context.Request.Path.StartsWithSegments("/items")))
            {
                context.Response.Redirect("/Auth/SignIn");
                return;
            }

            await _next(context);
        }
    }
}