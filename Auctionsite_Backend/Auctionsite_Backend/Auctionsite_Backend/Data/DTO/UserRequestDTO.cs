namespace Auctionsite_Backend.Data.DTO
{
    public class UserRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "user";
        public string Password { get; set; }
    }
}
