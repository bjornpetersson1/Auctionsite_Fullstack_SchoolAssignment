using Auctionsite_Backend.Data.DTO;
using Auctionsite_Backend.Data.Interface;
using Auctionsite_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Auctionsite_Backend.Data.Repo
{
    public class AuctionRepo : IAuctionsRepo
    {
        private readonly AuctionSiteDbContext _dbContext;

        public AuctionRepo(AuctionSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllBidsResponseDTO?> GetAllBids()
        {
            var bids = await _dbContext.Bids.ToListAsync();
            if (bids == null) return null;

            var biddersId = bids
                .Select(b => b.UserId)
                .Distinct()
                .ToList();

            var biddersIdNames = new Dictionary<int, string>();

            foreach (var id in biddersId)
            {
                var bidder = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                var name = bidder?.Name ?? "unknown";
                biddersIdNames.Add(id, name);
            }

            var allBids = new GetAllBidsResponseDTO();

            foreach (var bid in bids)
            {
                allBids.Bids.Add(
                    new GetAllBidsResponseBidDTO()
                    {
                        Id = bid.Id,
                        AuctionId = bid.AuctionId,
                        Amount = bid.Amount,
                        PlacedAt = bid.PlacedAt,
                        BidderName = biddersIdNames[bid.UserId],
                        UserId = bid.UserId,
                    });
            }

            return allBids;
        }

        public async Task<GetAllBidsResponseDTO?> GetAllBidsForAuction(int auctionId)
        {
            var auction = await _dbContext
                .Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == auctionId);
            if (auction == null)
            { 
                return null;
            }
            var bids = auction.Bids
                .OrderByDescending(b => b.Amount)
                .ToList();

            if(bids == null)
            { 
                return null; 
            }
            
            var biddersId = bids
                .Select(b => b.UserId)
                .Distinct()
                .ToList();
            
            var biddersIdNames = new Dictionary<int, string>();
            
            foreach (var id in biddersId)
            {
                var bidder = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                var name = bidder?.Name ?? "unknown";
                biddersIdNames.Add(id, name);
            }

            var allBidsForAuction = new GetAllBidsResponseDTO();

            foreach (var bid in bids)
            {
                allBidsForAuction.Bids.Add(
                    new GetAllBidsResponseBidDTO()
                    {
                        Id = bid.Id,
                        AuctionId = bid.AuctionId,
                        Amount = bid.Amount,
                        PlacedAt = bid.PlacedAt,
                        BidderName = biddersIdNames[bid.UserId],
                        UserId = bid.UserId,
                    });
            }

            return allBidsForAuction;

        }
        public async Task<PlaceBidResponseDTO> PlaceBidOnAuction(PlaceBidDTO placeBid)
        {
            if(placeBid.Amount <= 0)
            {
                return new PlaceBidResponseDTO() { Message = "Bud kan inte vara noll eller mindre" };
            }
            var auction = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == placeBid.AuctionId);
            if (auction == null)
            {
                return new PlaceBidResponseDTO() { Message = "Auktion hittades ej" };
            }
            if (!auction.IsActive || auction.StartDateTime >  DateTime.UtcNow)
            {
                return new PlaceBidResponseDTO() { Message = "Auktionen är inte aktiv" };
            }
            if (auction.EndDateTime < DateTime.UtcNow)
            {
                return new PlaceBidResponseDTO() { Message = "Auktionen är slut" };
            }
            if (auction.UserId == placeBid.UserId)
            {
                return new PlaceBidResponseDTO() { Message = "Du kan inte lägga bud på din egen auktion" };
            }

            var topBid = await _dbContext.Bids
                .Where(b => b.AuctionId == placeBid.AuctionId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

            if(topBid != null)
            {
                if(placeBid.Amount <= topBid?.Amount)
                {
                    return new PlaceBidResponseDTO() { Message = "Bud måste vara högre än nu högsta bud" };
                }
            }

            var userBid = new Bid()
            {
                Amount = placeBid.Amount,
                UserId = placeBid.UserId,
                AuctionId = placeBid.AuctionId,
            };
            await _dbContext.Bids.AddAsync(userBid);
            var rowsSaved = await _dbContext.SaveChangesAsync();
            if(rowsSaved > 0)
            {
                return new PlaceBidResponseDTO()
                {
                    Message = "success",
                    Amount = userBid.Amount,
                    UserId = userBid.UserId,
                    AuctionId = userBid.AuctionId,
                    PlacedAt = userBid.PlacedAt,
                };
            }
            else
            {
                return new PlaceBidResponseDTO() { Message = "Something went wrong" };
            }
        }

        public async Task<AuctionListDTO> GetAuctionsList(bool includeAll)
        {
            var response = new List<Auction>();
            var now = DateTime.Now;
            var toActivate = await _dbContext.Auctions
                .Where(a => a.StartDateTime <= now && a.EndDateTime > now &&
                    (a.IsActive == false || a.IsOpen == false))
                .ToListAsync();

            if (toActivate.Any())
            {
                foreach (var a in toActivate)
                {
                    a.IsActive = true;
                    a.IsOpen = true;
                }
                await _dbContext.SaveChangesAsync();
            }

            if (includeAll)
            {
                response = await _dbContext.Auctions
                    .OrderBy(a => a.EndDateTime)
                    .ToListAsync();
            }
            else
            {
                response = await _dbContext.Auctions
                    .Where(
                    a => a.IsActive == true
                    && a.IsOpen == true
                    && a.StartDateTime <= DateTime.UtcNow
                    && a.EndDateTime > DateTime.UtcNow)
                    .OrderBy(a => a.EndDateTime)
                    .ToListAsync();
            }
            if (response != null && response.Count > 0)
            {
                var dto = new AuctionListDTO();
                foreach (var auction in response)
                {
                    dto.Auctions.Add(new AuctionDTO
                    {
                        Id = auction.Id,
                        Title = auction.Title,
                        Description = auction.Description,
                        AskingPrice = auction.AskingPrice,
                        ImageUrl = auction.ImageUrl,
                        IsActive = auction.IsActive,
                        IsOpen = auction.IsOpen,
                        StartDateTime = auction.StartDateTime,
                        EndDateTime = auction.EndDateTime,
                        UserId = auction.UserId,
                    });
                }
                return dto;
            }
            else return new AuctionListDTO();
        }
        public async Task<AuctionDTO?> GetAuctionById(int id)
        {
            var response = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == id);
            if(response == null) return null;
            return new AuctionDTO()
            {
                Id = response.Id,
                Title = response.Title,
                Description = response.Description,
                AskingPrice = response.AskingPrice,
                ImageUrl = response.ImageUrl,
                StartDateTime = response.StartDateTime,
                EndDateTime = response.EndDateTime,
                IsActive = response.IsActive,
                IsOpen = response.StartDateTime <= DateTime.UtcNow 
                    && response.EndDateTime > DateTime.UtcNow,
                UserId = response.UserId,
            };
        }
        public async Task<CreateNewAuctionResponseDTO> CreateNewAuction(CreateNewAuctionDTO auction, int userId )
        {
            if(auction.StartDateTime > auction.EndDateTime)
            {
                return new CreateNewAuctionResponseDTO()
                { Message = "Starttid kan inte vara efter sluttid" };
            }

            if (auction.StartDateTime < DateTime.UtcNow)
            {
                return new CreateNewAuctionResponseDTO
                { Message = "Starttid kan inte vara i dåtid" };

            }
          
            var newAuction = new Auction()
            {
                Title = auction.Title,
                Description = auction.Description,
                AskingPrice = auction.AskingPrice,
                ImageUrl = auction.ImageUrl,
                StartDateTime = auction.StartDateTime,
                EndDateTime = auction.EndDateTime,
                UserId = userId,
            };
            await _dbContext.Auctions.AddAsync(newAuction);
            var rowsSaved = await _dbContext.SaveChangesAsync();
            if (rowsSaved > 0)
            {
                return new CreateNewAuctionResponseDTO
                { 
                    Message = "success",
                    Id = newAuction.Id,
                    CreatedAt = DateTime.UtcNow,
                };
            }
            else
            {
                return new CreateNewAuctionResponseDTO
                {
                    Message = "Något gick fel",
                };
            }
        }
        public async Task<EditAuctionResponseDTO> EditAuction(EditAuctionDTO auction)
        {
            var original = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == auction.Id);
            if(original == null)
            {
                return new EditAuctionResponseDTO { Message = "Auction not found" };
            }
            
            original.Title = auction.Title;
            original.Description = auction.Description;
            original.AskingPrice = auction.AskingPrice;
            original.ImageUrl = auction.ImageUrl;
            original.StartDateTime = auction.StartDateTime;
            original.EndDateTime = auction.EndDateTime;
            original.EditedAt = DateTime.UtcNow;

            var rowsSaved = await _dbContext.SaveChangesAsync();
            if (rowsSaved > 0)
            {
                return new EditAuctionResponseDTO
                {
                    Message = "success",
                    EditedAt = original.EditedAt
                };
            }
            else
            {
                return new EditAuctionResponseDTO
                { Message = "Something went wrong" };
            }
        }


        public async Task<DeleteAuctionResponseDTO> DeleteAuction(DeleteAuctionDTO auction)
        {
            var selectedAuction = await _dbContext.Auctions.FirstOrDefaultAsync(a => a.Id == auction.AuctionID);
            if (selectedAuction == null)
            {
                return new DeleteAuctionResponseDTO
                {
                    Message = "Auction not found",
                    IsDeleted = false
                };
            }
            else
            {
                _dbContext.Auctions.Remove(selectedAuction);
                await _dbContext.SaveChangesAsync();
                return new DeleteAuctionResponseDTO
                {
                    Message = "success",
                    IsDeleted = true
                };
            }
        }

        public async Task<AuctionListDTO?> GetAuctionsListFromQuery(string query, bool includeClosed)
        {
            var result = new List<Auction>();
            if(includeClosed == false)
            {
                result = await _dbContext.Auctions.Where(a => a.Title.Contains(query) && a.IsOpen == true && a.IsActive == true).ToListAsync();
            }
            else
            {
                result = await _dbContext.Auctions.Where(a => a.Title.Contains(query) && a.IsActive == true && (a.EndDateTime < DateTime.UtcNow || a.IsOpen == true)).ToListAsync();
            }
            var searchResult = new AuctionListDTO();
            foreach (var item in result)
            {
                searchResult.Auctions.Add(new AuctionDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    UserId = item.UserId,
                    AskingPrice = item.AskingPrice,
                    ImageUrl = item.ImageUrl,
                    StartDateTime = item.StartDateTime,
                    EndDateTime = item.EndDateTime,
                    IsActive = item.IsActive,
                    IsOpen = item.IsOpen,
                });
            }
            return searchResult;

        }
    }
}
