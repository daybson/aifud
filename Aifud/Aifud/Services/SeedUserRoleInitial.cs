using Microsoft.AspNetCore.Identity;

namespace Aifud.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedRoles()
        {
            CreateRoleIfNotExists("Cliente");
            CreateRoleIfNotExists("Admin");
        }
        private void CreateRoleIfNotExists(string roleName)
        {
            if (!roleManager.RoleExistsAsync(roleName).Result)
            {
                var role = new IdentityRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }


        public void SeedUser()
        {
            CreateUserIfNotExists("cliente@email.com", "Cliente", "Teste@123");
            CreateUserIfNotExists("admin@email.com", "Admin", "Teste@123");
        }
        private void CreateUserIfNotExists(string email, string role, string password)
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    NormalizedUserName = email.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }
    }
}
