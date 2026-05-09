using Auctionsite_Backend.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auctionsite_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("AdminOnly")]
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
    }
}
