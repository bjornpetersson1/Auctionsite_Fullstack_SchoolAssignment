using Auctionsite_Backend.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAuctionsList()
        {
            var response = await _auctionsService.GetAuctionsList();
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
        [HttpPost]
        public async Task<IActionResult> CreateNewAuction()
        {
            //Hämta id:et från jwt-claims
            return Ok();
        }

        [Authorize("UserOrAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAuction(int id)
        {
            //admin eller ägaren av auctionen kan edita
            return Ok();
        }

        [Authorize("UserOrAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            //admin eller ägaren av auctionen kan deleta
            return Ok();
        }
    }
}
