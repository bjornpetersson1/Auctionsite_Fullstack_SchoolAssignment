using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auctionsite_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("AdminOnly")]
    [RequireActiveUser]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPatch("auctions/{id}/deactivate")]
        public async Task<IActionResult> DeactivateAuction(int id)
        {
            var response = await _adminService.DeactivateAuction(id);
            if(response == null)
            {
                return NotFound("Auction not found");
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPatch("auctions/{id}/reactivate")]
        public async Task<IActionResult> ReactivateAuction(int id)
        {
            var response = await _adminService.ReactivateAuction(id);
            if (response == null)
            {
                return NotFound("Auction not found");
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPatch("users/{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var userId = GetUserIdFromJWT();
            if (userId == id) return BadRequest("You can't deactivate your own account");

            var response = await _adminService.DeactivateUser(id);
            if (response == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPatch("users/{id}/reactivate")]
        public async Task<IActionResult> ReactivateUser(int id)
        {
            var response = await _adminService.ReactivateUser(id);
            if (response == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(response);
            }
        }
        private int GetUserIdFromJWT()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return 0;
            else return int.Parse(userId);
        }
    }
}
