using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;
using Auctionsite_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace Auctionsite_Backend.Data.Repo
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AuctionSiteDbContext dbContext;
        private readonly IJWTservice _jwtService;

        public AuthRepo(AuctionSiteDbContext dbContext, IJWTservice jwtService)
        {
            this.dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<UserResponseDTO?> Register(UserRequestDTO userRequestDTO)
        {
            if(userRequestDTO.Password == null 
                || userRequestDTO.Email == null 
                || userRequestDTO.Name == null
                || dbContext.Users.Any(u => u.Email == userRequestDTO.Email.ToLower()))
            {
                return null; 
            }

            else
            {
                var user = new User()
                {
                    Name = userRequestDTO.Name,
                    Email = userRequestDTO.Email.ToLower(),
                    Role = userRequestDTO.Role,
                    PasswordHash = BC.HashPassword(userRequestDTO.Password, workFactor: 12)
                };
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                var response = new UserResponseDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                };

                return response;
            }
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var response = new LoginResponseDTO();
            if (loginRequestDTO.Password == null || loginRequestDTO.Email == null)
            {
                response.LoginSuccess = false;
                response.ResponseMessage = "Missing password or email input";
            }
            else 
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDTO.Email.ToLower());
                if (user == null)
                {
                    response.LoginSuccess = false;
                    response.ResponseMessage = "Email not registered, consider creating new user";
                }
                else
                {
                    if(BC.Verify(loginRequestDTO.Password, user.PasswordHash))
                    {
                        if(!user.IsActive)
                        {
                            response.LoginSuccess = false;
                            response.ResponseMessage = "Account is deactivated";
                        }
                        else
                        {
                            response.LoginSuccess = true;
                            response.Id = user.Id;
                            response.Name = user.Name;
                            response.Email = user.Email;
                            response.Role = user.Role;
                            response.AccessToken = _jwtService.GenerateAccessToken(user);
                            response.RefreshToken = await _jwtService.GenerateRefreshToken(user);
                            response.ResponseMessage = "Login successful";
                        }
                    }
                    else
                    {
                        response.LoginSuccess = false;
                        response.ResponseMessage = "Incorrect password";
                    }
                }
            }
            return response;
        }
    }
}
