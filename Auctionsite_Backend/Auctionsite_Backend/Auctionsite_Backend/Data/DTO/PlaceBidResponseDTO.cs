namespace Auctionsite_Backend.Data.DTO
{
    public class PlaceBidResponseDTO
    {
        public string Message { get; set; }
        public float? Amount { get; set; }
        public int? UserId { get; set; }
        public int? AuctionId { get; set; }
        public DateTime? PlacedAt { get; set; }
    }
}
