using Auctionsite_Backend.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Data
{
    public class AuctionSiteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public AuctionSiteDbContext()
        {
        }
        public AuctionSiteDbContext(DbContextOptions<AuctionSiteDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var config = new ConfigurationBuilder().AddUserSecrets<AuctionSiteDbContext>().Build();

            var connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = config["ServerName"],
                InitialCatalog = config["DataBase"],
                TrustServerCertificate = true,
                IntegratedSecurity = true
            }.ToString();

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
