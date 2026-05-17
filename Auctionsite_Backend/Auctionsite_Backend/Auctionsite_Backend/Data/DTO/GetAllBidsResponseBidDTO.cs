namespace Auctionsite_Backend.Data.DTO
{
    public class GetAllBidsResponseBidDTO
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public float Amount { get; set; }
        public DateTime PlacedAt { get; set; }
        public string BidderName { get; set; }
        public int UserId { get; set; }
    }
}
