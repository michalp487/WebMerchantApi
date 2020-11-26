using System;
using WebMerchantApi.Enums;
using WebMerchantApi.Models;

namespace WebMerchantApi.Entities
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public decimal Price { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
