namespace Auctionsite_Backend.Data.DTO
{
    public class AuctionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float? AskingPrice { get; set; }
        public string? ImageUrl { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsOpen { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
