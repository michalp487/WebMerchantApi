using System.Collections.Generic;
using System.Threading.Tasks;
using WebMerchantApi.Models;

namespace WebMerchantApi.Validators.Interfaces
{
    public interface IBasketValidator
    {
        Task<List<Error>> Validate();
    }
}
