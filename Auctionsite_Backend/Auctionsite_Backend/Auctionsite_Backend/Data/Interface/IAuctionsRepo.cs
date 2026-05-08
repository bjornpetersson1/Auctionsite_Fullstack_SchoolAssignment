using Auctionsite_Backend.Data.DTO;

namespace Auctionsite_Backend.Data.Interface
{
    public interface IAuctionsRepo
    {
        Task<AuctionListDTO> GetAuctionsList();
        Task<AuctionDTO?> GetAuctionById(int id);
        Task<CreateNewAuctionResponseDTO> CreateNewAuction();
        Task<EditAuctionResponseDTO> EditAuction(int id);
        Task<DeleteAuctionResponseDTO> DeleteAuction(int id);
    }
}
