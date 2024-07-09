using Microsoft.AspNetCore.Identity;
using SchopenSozlukEntityLayer.Concrete;

namespace SchopenSozlukPresentationLayer.Data
{
    public class SeedData
    {
        public static async Task Initialize(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            string adminRole = "Admin";

            // Rol oluşturma
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new AppRole { Name = adminRole });
            }

            // Admin kullanıcı oluşturma
            string adminEmail = "admin@example.com";
            var adminUser = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                Name = "Admin",
                Surname = "User"
            };

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                await userManager.CreateAsync(adminUser, "Admin@1234");
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
}
