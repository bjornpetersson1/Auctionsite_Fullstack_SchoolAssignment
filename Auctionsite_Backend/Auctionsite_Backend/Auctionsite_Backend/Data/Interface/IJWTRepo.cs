using Auctionsite_Backend.Models;

namespace Auctionsite_Backend.Data.Interface
{
    public interface IJWTRepo
    {
        Task<string> GenerateRefreshToken(string refreshToken, int userId);
        Task<RefreshToken?> GetRefreshToken(string token);
        Task RevokeRefreshToken(RefreshToken refreshToken);
    }
}
