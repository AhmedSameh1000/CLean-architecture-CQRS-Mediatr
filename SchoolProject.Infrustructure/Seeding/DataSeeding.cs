using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;

namespace RefreshToken.Seeding
{
    public class SeedingData
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public SeedingData(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedData()
        {
            if (_roleManager.Roles.Any())
            {
                return;
            }

            var UserRole = new IdentityRole()
            {
                Id = "1",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = ""
            };
            var AdminRole = new IdentityRole()
            {
                Id = "2",

                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = ""
            };
            await _roleManager.CreateAsync(UserRole);
            await _roleManager.CreateAsync(AdminRole);

            var UserToSeed = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Ahmed@gmail.com",
                UserName = "Ahmed@gmail.com",
                FullName = "Ahmed Sameh"
            };
            var Result = await _userManager.CreateAsync(UserToSeed, "ahmeds1490");

            if (Result.Succeeded)
            {
                await _userManager.AddToRolesAsync(UserToSeed, new List<string> { UserRole.Name, AdminRole.Name });
            }
        }
    }
}