using System.Collections.Generic;

namespace WebMerchantApi.Models.Dto
{
    public class BasketSummaryResponseDto
    {
        public BasketSummaryResponseDto()
        {
            BasketItems = new List<BasketItemResponseDto>();
        }

        public IEnumerable<BasketItemResponseDto> BasketItems { get; set; }

        public decimal Amount { get; set; }
    }
}
