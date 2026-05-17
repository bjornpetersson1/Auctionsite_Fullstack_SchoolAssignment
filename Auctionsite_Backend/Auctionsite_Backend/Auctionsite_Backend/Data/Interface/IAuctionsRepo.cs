using Auctionsite_Backend.Data.DTO;

namespace Auctionsite_Backend.Data.Interface
{
    public interface IAuctionsRepo
    {
        Task<GetAllBidsResponseDTO?> GetAllBids();
        Task<GetAllBidsResponseDTO?> GetAllBidsForAuction(int auctionId);
        Task<PlaceBidResponseDTO> PlaceBidOnAuction(PlaceBidDTO placeBid);
        Task<AuctionListDTO> GetAuctionsList(bool includeAll);
        Task<AuctionDTO?> GetAuctionById(int id);
        Task<CreateNewAuctionResponseDTO> CreateNewAuction(CreateNewAuctionDTO auction, int userId);
        Task<EditAuctionResponseDTO> EditAuction(EditAuctionDTO auction);
        Task<DeleteAuctionResponseDTO> DeleteAuction(DeleteAuctionDTO auction);
    }
}
