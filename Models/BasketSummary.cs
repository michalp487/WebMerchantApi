using System.Collections.Generic;
using WebMerchantApi.Entities;

namespace WebMerchantApi.Models
{
    public class BasketSummary
    {
        public BasketSummary()
        {
            BasketItems = new List<BasketItem>();
        }

        public IEnumerable<BasketItem> BasketItems { get; set; }

        public decimal Amount { get; set; }
    }
}
