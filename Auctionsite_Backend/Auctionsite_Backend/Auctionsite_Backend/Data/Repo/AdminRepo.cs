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

        public async Task<ReDeActivateAuctionResponseDTO?> ReactivateAuction(int id)
        {
            var response = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == id);
            if (response == null) return null;
            else
            {
                response.IsActive = true;
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
        public async Task<ReDeActivateUserResponseDTO?> DeactivateUser(int id)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (response == null) return null;
            else
            {
                response.IsActive = false;
                await _dbContext.SaveChangesAsync();
                return new ReDeActivateUserResponseDTO()
                {
                    Id = response.Id,
                    Name = response.Name,
                    Email = response.Email,
                    Role = response.Role,
                    IsActive = response.IsActive,
                };
            }
        }

        public async Task<ReDeActivateUserResponseDTO?> ReactivateUser(int id)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (response == null) return null;
            else
            {
                response.IsActive = true;
                await _dbContext.SaveChangesAsync();
                return new ReDeActivateUserResponseDTO()
                {
                    Id = response.Id,
                    Name = response.Name,
                    Email = response.Email,
                    Role = response.Role,
                    IsActive = response.IsActive,
                };
            }
        }
    }
}
