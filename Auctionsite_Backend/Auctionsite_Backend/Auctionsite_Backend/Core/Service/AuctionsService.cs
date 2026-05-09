using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;

namespace Auctionsite_Backend.Core.Service
{
    public class AuctionsService : IAuctionsService
    {
        private readonly IAuctionsRepo _auctionsRepo;

        public AuctionsService(IAuctionsRepo auctionsRepo)
        {
            _auctionsRepo = auctionsRepo;
        }
        public async Task<AuctionListDTO> GetAuctionsList(bool includeAll)
        {
            var response = await _auctionsRepo.GetAuctionsList(includeAll);
            return response;
        }
        public async Task<AuctionDTO?> GetAuctionById(int id)
        {
            var response = await _auctionsRepo.GetAuctionById(id);
            return response;
        }

        public async Task<CreateNewAuctionResponseDTO> CreateNewAuction(CreateNewAuctionDTO auction, int userId)
        {
            var response = await _auctionsRepo.CreateNewAuction(auction, userId);
            return response;
        }
        public async Task<EditAuctionResponseDTO> EditAuction(EditAuctionDTO auction)
        {
            var response = await _auctionsRepo.EditAuction(auction);
            return response;
        }

        public async Task<DeleteAuctionResponseDTO> DeleteAuction(DeleteAuctionDTO auction)
        {
            var response = await _auctionsRepo.DeleteAuction(auction);
            return response;
        }



    }
}
