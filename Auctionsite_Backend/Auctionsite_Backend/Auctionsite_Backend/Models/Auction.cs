using Auctionsite_Backend.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsOpen { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float? AskingPrice { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }
        public List<Bid>? Bids { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
