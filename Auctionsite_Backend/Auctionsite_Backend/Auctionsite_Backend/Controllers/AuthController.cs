using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auctionsite_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO userRequestDTO)
        {
            var response = await _authService.Register(userRequestDTO);
            if (response != null)
            {
                return Created("", response);
            }
            else
            {
                return BadRequest("Email already in use or request is missing some input");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var response = await _authService.Login(loginRequestDTO);
            if (response.LoginSuccess)
            {
                return Ok(response.ResponseMessage);
            }
            else return BadRequest(response.ResponseMessage);
        }
    }
}
