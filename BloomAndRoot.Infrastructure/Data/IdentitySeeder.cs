using BloomAndRoot.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace BloomAndRoot.Infrastructure.Data
{
  public static class IdentitySeeder
  {
    public static async Task SeedRolesAndAdminAsync(
      UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager
    )
    {
      if (!await roleManager.RoleExistsAsync("Admin"))
      {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
      }

      if (!await roleManager.RoleExistsAsync("Customer"))
      {
        await roleManager.CreateAsync(new IdentityRole("Customer"));
      }

      var adminEmail = "admin@bloomandroot.com";
      var adminUser = await userManager.FindByEmailAsync(adminEmail);

      if (adminUser == null)
      {
        var admin = new ApplicationUser
        {
          UserName = adminEmail,
          Email = adminEmail,
          EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(admin, "!Admin123"); // <- password, better change it later

        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(admin, "Admin");
        }
      }
    }
  }
}