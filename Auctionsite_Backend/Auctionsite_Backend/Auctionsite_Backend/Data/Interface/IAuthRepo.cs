using Auctionsite_Backend.Data.DTO;

namespace Auctionsite_Backend.Data.Interface
{
    public interface IAuthRepo
    {
        Task<UserResponseDTO?> Register(UserRequestDTO userRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
