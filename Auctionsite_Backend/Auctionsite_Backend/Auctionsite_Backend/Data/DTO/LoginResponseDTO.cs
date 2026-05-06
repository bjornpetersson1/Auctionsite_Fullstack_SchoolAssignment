namespace Auctionsite_Backend.Data.DTO
{
    public class LoginResponseDTO
    {
        public bool LoginSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
