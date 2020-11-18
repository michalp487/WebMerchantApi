using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebMerchantApi.Constants;
using WebMerchantApi.Data;
using WebMerchantApi.Helpers.Interfaces;
using WebMerchantApi.Models;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHashHelper _hashHelper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager, IHashHelper hashHelper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _hashHelper = hashHelper;
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));

            if (ValidateUser(password, user))
            {
                response.Success = false;
                response.Message = "Wrong credentials.";
                return response;
            }

            response.Data = await CreateToken(user);

            return response;
        }

        public async Task<ServiceResponse<string>> Register(ApplicationUser user, string password)
        {
            var response = new ServiceResponse<string>();

            if (await UserExists(user.UserName))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            _hashHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower()))
            {
                return true;
            }

            return false;
        }

        private bool ValidateUser(string password, ApplicationUser user)
        {
            return user == null || !_hashHelper.VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt));
        }

        private async Task<string> CreateToken(ApplicationUser user)
        {
            var role = await _userManager.IsInRoleAsync(user, RoleConstants.Admin)
                ? RoleConstants.Admin
                : RoleConstants.Customer;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
