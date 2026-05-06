using Auctionsite_Backend.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Auctionsite_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public List<Auction>? Auctions { get; set; }
        public List<Bid>? Bids { get; set; }
    }
}
