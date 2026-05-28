using Auctionsite_Backend.Data.DTO;

namespace Auctionsite_Backend.Core.Interface
{
    public interface IAuthService
    {
        Task<UserResponseDTO?> Register(UserRequestDTO userRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<string?> GetNameById(int id);
        Task<List<UserResponseDTO>> GetAllUsers();
        Task<bool> UpdatePassword(int userId, string oldPassword, string newPassword);
    }
}
