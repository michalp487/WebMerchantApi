﻿namespace WebMerchantApi.Models.Dto
{
    public class BasketItemResponseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
