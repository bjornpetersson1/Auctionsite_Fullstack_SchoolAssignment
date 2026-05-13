using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auctionsite_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionsService _auctionsService;

        public AuctionsController(IAuctionsService auctionsService)
        {
            _auctionsService = auctionsService;
        }

        [HttpGet("{auctionId}/bids")]
        public async Task<IActionResult> GetAllBids(int auctionId)
        {
            var response = await _auctionsService.GetAllBids(auctionId);
            if(response == null)
            {
                return NotFound("AuctionId invalid");
            }
            else
            {
                return Ok(response);
            }
        }

        [Authorize("UserOnly")]
        [RequireActiveUser]
        [HttpPost("{auctionId}/bids")]
        public async Task<IActionResult> PlaceBidOnAuction(int auctionId, float amount)
        {
            var userId = GetUserIdFromJWT();
            var userBid = new PlaceBidDTO() { AuctionId = auctionId, Amount = amount, UserId = userId };
            var response = await _auctionsService.PlaceBidOnAuction(userBid);
            if(response.Message != "success")
            {
                return BadRequest(response.Message);
            }
            return Created($"api/auctions/{auctionId}/bids",response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAuctionsList([FromQuery] bool includeAll = false)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if(includeAll == true && userRole != "admin")
            {
                return Forbid();
            }
            var response = await _auctionsService.GetAuctionsList(includeAll);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionById(int id)
        {
            var response = await _auctionsService.GetAuctionById(id);
            if (response == null) return BadRequest(response);
            else return Ok(response);
        }

        [Authorize("UserOrAdmin")]
        [RequireActiveUser]
        [HttpPost]
        public async Task<IActionResult> CreateNewAuction([FromBody] CreateNewAuctionDTO auction)
        {
            var userId = GetUserIdFromJWT();
            if (userId == 0) return BadRequest("No user found");
            var response = await _auctionsService.CreateNewAuction(auction, userId);
            if (response.Message != "success") return BadRequest(response.Message);
            return Ok(response);
        }

        [Authorize("UserOrAdmin")]
        [RequireActiveUser]
        [HttpPut]
        public async Task<IActionResult> EditAuction([FromBody] EditAuctionDTO auction)
        {
            var userId = GetUserIdFromJWT();
            if (userId == 0) return BadRequest("No user found");
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "admin" || userId == auction.AuctionCreaterId)
            {
                var response = await _auctionsService.EditAuction(auction);
                return Ok(response);
            }
            else return BadRequest("You are not authorized to edit this auction");
        }

        [Authorize("UserOrAdmin")]
        [RequireActiveUser]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuction([FromBody] DeleteAuctionDTO auction)
        {
            var userId = GetUserIdFromJWT();
            if (userId == 0) return BadRequest("No user found");
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "admin" || userId == auction.CreaterId)
            {
                var response = await _auctionsService.DeleteAuction(auction);
                if(response.IsDeleted)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return NotFound(response.Message);
                }
            }
            else return BadRequest("You are not authorized to delete this auction");

        }

        private int GetUserIdFromJWT()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return 0;
            else return int.Parse(userId);
        }
    }
}
