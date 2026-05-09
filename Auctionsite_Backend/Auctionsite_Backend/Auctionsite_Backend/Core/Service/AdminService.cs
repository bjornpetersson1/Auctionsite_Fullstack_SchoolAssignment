using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;

namespace Auctionsite_Backend.Core.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;

        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public Task<ReDeActivateAuctionResponseDTO?> DeactivateAuction(int id)
        {
            var response = _adminRepo.DeactivateAuction(id);
            return response;
        }

        public Task<ReDeActivateUserResponseDTO?> DeactivateUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReDeActivateAuctionResponseDTO?> ReactivateAuction(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReDeActivateUserResponseDTO?> ReactivateUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
