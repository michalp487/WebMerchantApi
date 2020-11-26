using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WebMerchantApi.Entities;

namespace WebMerchantApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PasswordSalt { get; set; }

        public IEnumerable<BasketItem> BasketItems { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
