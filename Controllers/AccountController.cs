using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMerchantApi.Models;
using WebMerchantApi.Models.Dto;
using WebMerchantApi.Services.Interfaces;

namespace WebMerchantApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await _accountService.Register(new ApplicationUser { UserName = request.Username, Email = request.Username}, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var response = await _accountService.Login(request.Username, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost("Logout")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _tokenService.DeactivateCurrentAsync();

            return Ok();
        }
    }
}
