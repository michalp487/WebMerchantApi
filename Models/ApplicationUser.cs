using Microsoft.AspNetCore.Identity;

namespace WebMerchantApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PasswordSalt { get; set; }
    }
}
