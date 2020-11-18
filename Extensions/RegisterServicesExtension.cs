using Microsoft.Extensions.DependencyInjection;
using WebMerchantApi.Data;
using WebMerchantApi.Services;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Extensions
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient<DatabaseSeeder>();

            return services;
        }
    }
}
