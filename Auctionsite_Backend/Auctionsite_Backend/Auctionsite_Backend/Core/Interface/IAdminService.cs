using Auctionsite_Backend.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Auctionsite_Backend.Core.Interface
{
    public interface IAdminService
    {
        Task<ReDeActivateAuctionResponseDTO?> DeactivateAuction(int id);
        Task<ReDeActivateAuctionResponseDTO?> ReactivateAuction(int id);
        Task<ReDeActivateUserResponseDTO?> DeactivateUser(int id);
        Task<ReDeActivateUserResponseDTO?> ReactivateUser(int id);
    }
}
