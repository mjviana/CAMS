using System.Linq.Expressions;
using CAMS.Domain.Entities.Auction;

namespace CAMS.Application.Interfaces;

/// <summary>
/// Defines data access operations for managing auctions in the system.
/// </summary>
public interface IAuctionRepository
{
    /// <summary>
    /// Adds a new auction to the repository.
    /// </summary>
    /// <param name="vehicle">The auction entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAuctionAsync(Auction vehicle);

    /// <summary>
    /// Retrieves all auctions that match the specified filter criteria.
    /// </summary>
    /// <param name="filter">An expression used to filter auctions based on specific conditions.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The result contains a collection of auctions that satisfy the filter.
    /// </returns>
    Task<IEnumerable<Auction>> GetAllByFilterAsync(Expression<Func<Auction, bool>> filter);

    /// <summary>
    /// Retrieves a specific auction by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the auction.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The result contains the auction if found; otherwise, null.
    /// </returns>
    Task<Auction?> GetAuctionByIdAsync(string id);

    /// <summary>
    /// Updates the state of an existing auction in the repository.
    /// </summary>
    /// <param name="auction">The auction entity with updated data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAuctionAsync(Auction auction);
}