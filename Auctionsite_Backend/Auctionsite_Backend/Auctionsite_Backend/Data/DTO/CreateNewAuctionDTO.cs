namespace Auctionsite_Backend.Data.DTO
{
    public class CreateNewAuctionDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public float? AskingPrice { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
