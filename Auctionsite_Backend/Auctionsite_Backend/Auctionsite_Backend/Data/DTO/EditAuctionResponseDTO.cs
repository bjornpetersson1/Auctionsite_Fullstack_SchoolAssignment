namespace Auctionsite_Backend.Data.DTO
{
    public class EditAuctionResponseDTO
    {
        public string Message { get; set; } = "";
        public int Id { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}
