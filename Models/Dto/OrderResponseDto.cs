using System;
using WebMerchantApi.Enums;

namespace WebMerchantApi.Models.Dto
{
    public class OrderResponseDto
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
