using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace reviel.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string adminEmail, string adminPassword)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Admin kullanıcısı var mı kontrol et
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                
                // Admin kullanıcısı yoksa oluştur
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var admin = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(admin, adminPassword);
                    
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Admin kullanıcısı oluşturulamadı: {string.Join(", ", result.Errors)}");
                    }
                }
            }
        }
    }
}
