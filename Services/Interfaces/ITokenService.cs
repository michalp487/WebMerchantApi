using System.Threading.Tasks;

namespace WebMerchantApi.Services.Interfaces
{
    public interface ITokenService
    {
        Task<bool> IsCurrentActiveToken();

        Task DeactivateCurrentAsync();

        Task<bool> IsActiveAsync(string token);

        Task DeactivateAsync(string token);
    }
}
