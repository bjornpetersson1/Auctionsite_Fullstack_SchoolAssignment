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
        private readonly IJWTservice _jwtService;

        public AuthController(IAuthService authService, IJWTservice jwtService)
        {;
            _authService = authService;
            _jwtService = jwtService;
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
                    Expires = DateTimeOffset.UtcNow.AddMinutes(4)
                });
                Response.Cookies.Append("refreshtoken", response.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                });
                return Ok(response);
            }
            else return BadRequest(new { message = response.ResponseMessage });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens()
        {
            var refreshResponse = Request.Cookies["refreshtoken"];
            if(refreshResponse == null)
            {
                return Unauthorized();
            }
            var response = await _jwtService.RefreshTokens(refreshResponse);
            if (response == (null, null))
            { 
                return Unauthorized(); 
            }
            Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(4)
            });
            Response.Cookies.Append("refreshtoken", response.NewRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
            });
            return Ok(response);
        }

        [Authorize("UserOrAdmin")]
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

        [Authorize]
        [HttpGet("{id}/user-name")]
        public async Task<IActionResult> GetNameById(int id)
        {
            var result = await _authService.GetNameById(id);
            if (result != null)
            {
                return Ok(new { name = result });
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize("AdminOnly")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _authService.GetAllUsers();
            return Ok(response);
        }
        [Authorize("UserOrAdmin")]
        [HttpPatch("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDTO dto)
        {
            if (dto.NewPassword.Length > 0)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (idClaim == null) return Unauthorized();
                var id = int.Parse(idClaim);
                var response = await _authService.UpdatePassword(id, dto.OldPassword, dto.NewPassword);
                if (response == true) return Ok();
                else return BadRequest("Felaktigt lösenord");
            }
            else return BadRequest();
        }
    }
}
