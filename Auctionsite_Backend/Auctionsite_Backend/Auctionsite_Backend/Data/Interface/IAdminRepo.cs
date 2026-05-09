using Auctionsite_Backend.Data.DTO;

namespace Auctionsite_Backend.Data.Interface
{
    public interface IAdminRepo
    {
        Task<ReDeActivateAuctionResponseDTO?> DeactivateAuction(int id);
        Task<ReDeActivateAuctionResponseDTO?> ReactivateAuction(int id);
        Task<ReDeActivateUserResponseDTO?> DeactivateUser(int id);
        Task<ReDeActivateUserResponseDTO?> ReactivateUser(int id);
    }
}
