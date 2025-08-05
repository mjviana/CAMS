using System.Linq.Expressions;
using CAMS.Application.Interfaces;
using CAMS.Domain.Entities.Auction;

namespace CAMS.Infrastructure;

public class InMemoryAuctionRepository : IAuctionRepository
{
    private readonly List<Auction> _auctions = new();

    public Task AddAuctionAsync(Auction vehicle)
    {
        _auctions.Add(vehicle);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Auction>> GetAllByFilterAsync(Expression<Func<Auction, bool>> filter)
    {
        var result = _auctions.AsQueryable().Where(filter);
        return Task.FromResult(result.AsEnumerable());
    }

    public Task<Auction?> GetAuctionByIdAsync(string id)
    {
        var result = _auctions.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(result);
    }

    public Task UpdateAuctionAsync(Auction auction)
    {
        // Simulated logic - (it's updated by reference)
        return Task.CompletedTask;
    }
}