namespace Auctionsite_Backend.Data.DTO
{
    public class GetAllBidsResponseDTO
    {
        public List<GetAllBidsResponseBidDTO> Bids { get; set; } = new();
    }
}
