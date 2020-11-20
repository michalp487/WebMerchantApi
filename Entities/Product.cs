using System;

namespace WebMerchantApi.Entities
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
