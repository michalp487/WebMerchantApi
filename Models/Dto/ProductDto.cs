using System.ComponentModel.DataAnnotations;

namespace WebMerchantApi.Models.Dto
{
    public class ProductDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Price in wrong format.")]
        public decimal Price { get; set; }
    }
}
