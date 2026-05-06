using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Models;

namespace Auctionsite_Backend.Core.Interface
{
    public interface IJWTservice
    {
        string GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(User user);
    }
}
