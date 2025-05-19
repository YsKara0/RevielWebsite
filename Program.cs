using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reviel.Data;

var builder = WebApplication.CreateBuilder(args);

// Load .env file
Env.Load();

var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DB_NAME")};User={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Identity sistemini ekle (kendi rotalarımızı kullanacağız, hazır Razor Pages kullanmayacağız)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cookie ayarlarını özelleştir
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Authentication middleware'i authorization'dan önce eklemeliyiz
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Özel rotaları tanımla
app.MapControllerRoute(
    name: "login",
    pattern: "giris",
    defaults: new { controller = "Account", action = "Login" });
    
app.MapControllerRoute(
    name: "adminPanel",
    pattern: "revieladminpanel",
    defaults: new { controller = "Admin", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Admin kullanıcısını oluştur
if (app.Environment.IsDevelopment())
{
    // Güvenlik için gerçek uygulamada bu bilgileri environment variables veya appsettings.json'dan almalısınız
    var adminEmail = "admin@reviel.com";
    var adminPassword = "Admin123!"; // Güçlü bir şifre kullanılmalı
    
    using (var scope = app.Services.CreateScope())
    {
        await DbInitializer.Initialize(scope.ServiceProvider, adminEmail, adminPassword);
        Console.WriteLine($"Admin kullanıcısı oluşturuldu: {adminEmail}");
    }
}

app.Run();
