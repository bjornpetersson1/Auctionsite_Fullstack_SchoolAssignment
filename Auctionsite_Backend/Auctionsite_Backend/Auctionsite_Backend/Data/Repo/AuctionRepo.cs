using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Data.Repo
{
    public class AuctionRepo : IAuctionsRepo
    {
        private readonly AuctionSiteDbContext _dbContext;

        public AuctionRepo(AuctionSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuctionListDTO> GetAuctionsList()
        {
            var response = await _dbContext.Auctions.ToListAsync();
            if (response != null && response.Count > 0)
            {
                var dto = new AuctionListDTO();
                foreach (var auction in response)
                {
                    dto.Auctions.Add(new AuctionDTO
                    {
                        Id = auction.Id,
                        Title = auction.Title,
                        Description = auction.Description,
                        AskingPrice = auction.AskingPrice,
                        ImageUrl = auction.ImageUrl,
                        StartDateTime = auction.StartDateTime,
                        EndDateTime = auction.EndDateTime,
                    });
                }
                return dto;
            }
            else return new AuctionListDTO();
        }
        public async Task<AuctionDTO?> GetAuctionById(int id)
        {
            var response = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == id);
            if(response == null) return null;
            return new AuctionDTO()
            {
                Id = response.Id,
                Title = response.Title,
                Description = response.Description,
                AskingPrice = response.AskingPrice,
                ImageUrl = response.ImageUrl,
                StartDateTime = response.StartDateTime,
                EndDateTime = response.EndDateTime,
            };
        }
        public Task<CreateNewAuctionResponseDTO> CreateNewAuction()
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAuctionResponseDTO> DeleteAuction(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EditAuctionResponseDTO> EditAuction(int id)
        {
            throw new NotImplementedException();
        }


    }
}
