using Auctionsite_Backend.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Auctionsite_Backend.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime PlacedAt { get; set; }
        public int UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User User { get; set; }
        public int AuctionId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Auction Auction { get; set; }
    }
}
