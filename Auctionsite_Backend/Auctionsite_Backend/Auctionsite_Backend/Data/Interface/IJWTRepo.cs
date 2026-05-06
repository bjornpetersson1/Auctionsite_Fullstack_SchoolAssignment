namespace Auctionsite_Backend.Data.Interface
{
    public interface IJWTRepo
    {
        Task<string> GenerateRefreshToken(string refreshToken, int userId);
    }
}
