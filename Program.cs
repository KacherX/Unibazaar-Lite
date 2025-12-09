using Final.Services;
using Final.Filters;
using Final.Middleware;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages ve MVC Controller desteği
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LogActivityFilter>(); // LogActivityFilter global
});
builder.Services.AddRazorPages();

// In-memory repository'ler (Singleton)
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddSingleton<IItemRepository, InMemoryItemRepository>();

// Filtreler (Scoped)
builder.Services.AddScoped<LogActivityFilter>();
builder.Services.AddScoped<ValidateItemExistsFilter>();
// builder.Services.AddScoped<ValidateEntityExistsFilter>(); // BUNU EKLEME!
// builder.Services.AddScoped<ValidateEntityExistsFilter<Item>>(); // BUNU EKLEME!

builder.Services.AddAuthentication("FakeAuth")
    .AddCookie("FakeAuth", options =>
    {
        options.LoginPath = "/Auth/SignIn";
    });

var app = builder.Build();

var culture = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

app.UseMiddleware<FakeAuthMiddleware>();

// Standart middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// MVC ve Razor Pages endpointleri
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
