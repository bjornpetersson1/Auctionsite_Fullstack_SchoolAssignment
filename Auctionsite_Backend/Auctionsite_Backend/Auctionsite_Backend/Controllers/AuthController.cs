using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions
                  {
                      HttpOnly = true,
                      Secure = true,
                      SameSite = SameSiteMode.Strict,
                      Expires =
                  DateTimeOffset.UtcNow.AddMinutes(4)
                  });
                return Ok(response);
            }
            else return BadRequest(response.ResponseMessage);
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var me = new GetMeDTO();
            me.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            me.Email = User.FindFirst(ClaimTypes.Email)?.Value;
            me.Role = User.FindFirst(ClaimTypes.Role)?.Value;
            me.Name = User.FindFirst(ClaimTypes.Name)?.Value;
            if(me.Name == null)
            {
                return BadRequest();
            }
            return Ok(me);
        }

        [Authorize("AdminOnly")]
        [HttpGet("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok("admin-nerd");
        }
    }
}
