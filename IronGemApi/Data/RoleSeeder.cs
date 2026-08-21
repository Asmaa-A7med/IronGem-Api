using Microsoft.AspNetCore.Identity;

namespace IronGemApi.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            string[] roles = { "Admin", "Coach", "User" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole<int>
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    };

                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}