using Microsoft.AspNetCore.Identity;

namespace LapTrinhWebBuoi6.Data
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Tạo 2 role nếu chưa tồn tại
            string[] roles = { "Admin", "Member" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Tạo tài khoản Admin
            string adminEmail = "admin@example.com";
            string adminPass = "Admin@123";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(admin, adminPass);
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Tạo tài khoản Member
            string memberEmail = "member@example.com";
            string memberPass = "Member@123";
            var member = await userManager.FindByEmailAsync(memberEmail);
            if (member == null)
            {
                member = new IdentityUser { UserName = memberEmail, Email = memberEmail, EmailConfirmed = true };
                await userManager.CreateAsync(member, memberPass);
                await userManager.AddToRoleAsync(member, "Member");
            }
        }
    }
}
