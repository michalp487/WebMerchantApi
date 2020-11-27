using System.ComponentModel.DataAnnotations;

namespace WebMerchantApi.Models.Dto
{
    public class BasketItemDto
    {
        [Required]
        public string ProductId { get; set; }
    }
}
