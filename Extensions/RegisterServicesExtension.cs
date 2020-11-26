using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using WebMerchantApi.Data;
using WebMerchantApi.Helpers;
using WebMerchantApi.Helpers.Interfaces;
using WebMerchantApi.Middleware;
using WebMerchantApi.Repositories;
using WebMerchantApi.Repositories.Interfaces;
using WebMerchantApi.Services;
using WebMerchantApi.Services.Interfaces;
using WebMerchantApi.Validators;
using WebMerchantApi.Validators.Interfaces;

namespace WebMerchantApi.Extensions
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IBasketValidator, BasketValidator>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddTransient<DatabaseSeeder>();
            services.AddScoped<IHashHelper, HashHelper>();

            services.AddTransient<TokenMiddleware>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDistributedCache, MemoryDistributedCache>();

            return services;
        }
    }
}
