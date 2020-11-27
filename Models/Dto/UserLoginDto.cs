using System.ComponentModel.DataAnnotations;

namespace WebMerchantApi.Models.Dto
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
