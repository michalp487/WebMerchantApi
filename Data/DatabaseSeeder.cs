using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMerchantApi.Models;
using Microsoft.Extensions.Configuration;
using WebMerchantApi.Constants;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Data
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public DatabaseSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, IAccountService accountService)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _accountService = accountService;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await SeedRole(RoleConstants.Admin);
            await SeedRole(RoleConstants.Customer);

            await SeedUserWithRole("mailadmin@mail.com", RoleConstants.Admin);
        }

        private async Task SeedUserWithRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };

                var password = _configuration.GetSection("AppSettings:DefaultAdminPass").Value;

                await _accountService.Register(user, password);
                await _userManager.AddToRoleAsync(user, role);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedRole(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == roleName);

            if (role == null)
            {
                role = new IdentityRole
                {
                    Name = roleName,
                    NormalizedName = roleName
                };

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
