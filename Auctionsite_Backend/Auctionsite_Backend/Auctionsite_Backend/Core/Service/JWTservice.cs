using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data;
using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;
using Auctionsite_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auctionsite_Backend.Core.Service
{
    public class JWTservice : IJWTservice
    {
        private readonly IConfiguration _config;
        private readonly AuctionSiteDbContext _dbContext;
        private readonly IJWTRepo _jwtRepo;

        public JWTservice(IConfiguration config, IJWTRepo jwtRepo, AuctionSiteDbContext dbContext)
        {
            _config = config;
            _jwtRepo = jwtRepo;
            _dbContext = dbContext;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("IsActive", user.IsActive.ToString().ToLower())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(4),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            return await _jwtRepo.GenerateRefreshToken(Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), user.Id);
        }

        public async Task<(string? AccessToken, string? NewRefreshToken)> RefreshTokens(string refreshToken)
        {
            var response = await _jwtRepo.GetRefreshToken(refreshToken);
            if (response == null || response.Expires < DateTime.UtcNow)
            {
                return (null, null);
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == response.Id);
            if (user == null || user.IsActive == false)
            {
                return (null, null);
            }
            await _jwtRepo.RevokeRefreshToken(response);
            var accToken = GenerateAccessToken(user);
            var refToken = await GenerateRefreshToken(user);

            return (accToken, refToken);
        }
    }
}
