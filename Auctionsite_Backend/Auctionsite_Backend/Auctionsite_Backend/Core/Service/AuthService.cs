using Auctionsite_Backend.Core.Interface;
using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;

namespace Auctionsite_Backend.Core.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;

        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        public async Task<UserResponseDTO?> Register(UserRequestDTO userRequestDTO)
        {
            return  await _authRepo.Register(userRequestDTO);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            return await _authRepo.Login(loginRequestDTO);
        }
    }
}
