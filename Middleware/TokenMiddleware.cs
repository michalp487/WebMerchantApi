using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Middleware
{
    public class TokenMiddleware : IMiddleware
    {
        private readonly ITokenService _tokenService;

        public TokenMiddleware(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenService.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
