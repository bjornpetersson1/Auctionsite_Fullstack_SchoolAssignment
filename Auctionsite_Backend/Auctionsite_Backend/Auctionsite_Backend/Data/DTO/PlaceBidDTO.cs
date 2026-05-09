using Auctionsite_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Data.DTO
{
    public class PlaceBidDTO
    {
        public float Amount { get; set; }
        public int UserId { get; set; }
        public int AuctionId { get; set; }
    }
}
