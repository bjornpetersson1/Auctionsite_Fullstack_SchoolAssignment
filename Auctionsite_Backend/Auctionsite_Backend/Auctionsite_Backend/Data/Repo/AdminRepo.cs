using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Data.Repo
{
    public class AdminRepo : IAdminRepo
    {
        private readonly AuctionSiteDbContext _dbContext;

        public AdminRepo(AuctionSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReDeActivateAuctionResponseDTO?> DeactivateAuction(int id)
        {
            var response = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == id);
            if(response == null) return null;
            else
            {
                response.IsActive = false;
                await _dbContext.SaveChangesAsync();
                return new ReDeActivateAuctionResponseDTO()
                {
                    Id = response.Id,
                    IsActive = response.IsActive,
                    Title = response.Title,
                    Description = response.Description,
                    AuctionCreaterId = response.UserId,
                    AskingPrice = response.AskingPrice,
                    ImageUrl = response.ImageUrl,
                    StartDateTime = response.StartDateTime,
                    EndDateTime = response.EndDateTime,
                };
            }
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
