using CAMS.Application.Dtos;
using CAMS.Domain.Entities.Auction;

namespace CAMS.Application.Interfaces;

/// <summary>
/// Provides high-level orchestration of auction-related operations, including starting, closing,
/// canceling auctions, and placing bids.
/// </summary>
public interface IAuctionOrchestrator
{
    /// <summary>
    /// Starts a new auction for the specified vehicle.
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle to auction.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task StartAuctionAsync(string vehicleId);
    
    /// <summary>
    /// Closes an existing auction, preventing any further bids.
    /// </summary>
    /// <param name="auctionId">The unique identifier of the auction to close.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CloseAuctionAsync(string auctionId);
    
    /// <summary>
    /// Cancels an existing auction. This means the auction ended prematurely and cannot be resumed.
    /// </summary>
    /// <param name="auctionId">The unique identifier of the auction to cancel.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CancelAuctionAsync(string auctionId);
    
    /// <summary>
    /// Places a new bid in an active auction.
    /// </summary>
    /// <param name="auctionId">The unique identifier of the auction.</param>
    /// <param name="placeBidDto">A DTO containing the bid data, such as bidder info and bid amount.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PlaceBidAsync(string auctionId, PlaceBidDto placeBidDto);
    
    /// <summary>
    /// Retrieves all currently open (active) auctions.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The result contains a collection of open auctions.</returns>
    Task<IEnumerable<Auction>> GetAllOpenAuctionsAsync();
    
    /// <summary>
    /// Retrieves a specific auction by its unique identifier.
    /// </summary>
    /// <param name="auctionId">The unique identifier of the auction.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains the auction if found; otherwise, null.
    /// </returns>
    Task<Auction?> GetAuctionByIdAsync(string auctionId);
}