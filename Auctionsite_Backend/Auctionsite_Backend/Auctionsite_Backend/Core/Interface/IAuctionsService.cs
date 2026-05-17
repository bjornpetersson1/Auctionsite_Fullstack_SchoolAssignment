using Auctionsite_Backend.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auctionsite_Backend.Core.Interface
{
    public interface IAuctionsService
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
