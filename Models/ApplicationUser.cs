using Microsoft.AspNetCore.Identity;

namespace WebMerchantApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] PasswordSalt { get; set; }
    }
}
