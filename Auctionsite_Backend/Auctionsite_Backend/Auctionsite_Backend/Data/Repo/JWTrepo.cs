using Auctionsite_Backend.Data.Interface;
using Auctionsite_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Data.Repo
{
    public class JWTrepo : IJWTRepo
    {
        private readonly AuctionSiteDbContext _dbContext;

        public JWTrepo(AuctionSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateRefreshToken(string refreshToken, int userId)
        {
            await _dbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            }); 

            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            var response = await _dbContext.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
            return response;

        }

        public async Task RevokeRefreshToken(RefreshToken refreshToken)
        {   
            refreshToken.IsRevoked = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
