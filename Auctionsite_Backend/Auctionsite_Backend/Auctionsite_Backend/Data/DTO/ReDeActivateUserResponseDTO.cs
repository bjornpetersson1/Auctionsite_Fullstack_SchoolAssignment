namespace Auctionsite_Backend.Data.DTO
{
    public class ReDeActivateUserResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
