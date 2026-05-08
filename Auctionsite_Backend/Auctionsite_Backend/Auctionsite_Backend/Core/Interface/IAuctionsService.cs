using Auctionsite_Backend.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auctionsite_Backend.Core.Interface
{
    public interface IAuctionsService
    {
        Task<AuctionListDTO> GetAuctionsList();
        Task<AuctionDTO?> GetAuctionById(int id);
        Task<CreateNewAuctionResponseDTO> CreateNewAuction();
        Task<EditAuctionResponseDTO> EditAuction(int id);
        Task<DeleteAuctionResponseDTO> DeleteAuction(int id);
    }
}
