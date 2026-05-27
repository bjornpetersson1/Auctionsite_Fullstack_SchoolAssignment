using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Models;

namespace Auctionsite_Backend.Data.Interface
{
    public interface IAuthRepo
    {
        Task<UserResponseDTO?> Register(UserRequestDTO userRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<string?> GetNameById(int id);
        Task<List<UserResponseDTO>> GetAllUsers();
        Task<User?> GetUserById(int id);
    }
}
