using Auctionsite_Backend.Data.DTO;

namespace Auctionsite_Backend.Core.Interface
{
    public interface IAuthService
    {
        Task<UserResponseDTO?> Register(UserRequestDTO userRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
