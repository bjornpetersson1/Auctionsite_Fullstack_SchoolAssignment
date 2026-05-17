namespace Auctionsite_Backend.Data.Seeders
{
    public static class SeedData
    {
        public static async Task SeedAsync(AuctionSiteDbContext dbContext)
        {
            await UserSeeder.CreateUsers(dbContext);
            await AuctionSeeder.CreateAuctions(dbContext);
            await BidSeeder.CreateBids(dbContext);
        }
    }
}
