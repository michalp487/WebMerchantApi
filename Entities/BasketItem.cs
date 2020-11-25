using System;
using WebMerchantApi.Models;

namespace WebMerchantApi.Entities
{
    public class BasketItem
    {
        public BasketItem()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
