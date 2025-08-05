using CAMS.Application.Dtos;
using CAMS.Application.Interfaces;
using CAMS.Domain.Entities;
using CAMS.Domain.Entities.Auction;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Application.Services;

public class AuctionOrchestrator : IAuctionOrchestrator
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IAuctionRepository _auctionRepository;

    public AuctionOrchestrator(IVehicleRepository vehicleRepository, IAuctionRepository auctionRepository)
    {
        _vehicleRepository = vehicleRepository;
        _auctionRepository = auctionRepository;
    }

    public async Task StartAuctionAsync(string vehicleId)
    {
        var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId)
                      ?? throw new VehicleDoesNotExistInAuctionInventoryException(vehicleId);

        var vehicleActiveOrClosedAuctions = await
            _auctionRepository.GetAllByFilterAsync(a =>
                a.Vehicle.Id == vehicle.Id &&
                (a.AuctionStatus == AuctionStatus.Active || a.AuctionStatus == AuctionStatus.Closed));

        if ((bool)vehicleActiveOrClosedAuctions?.Any())
        {
            throw new VehicleAlreadyAuctionedException(vehicleId);
        }

        var auction = new Auction(vehicle);
        auction.Start();

        await _auctionRepository.AddAuctionAsync(auction);
    }

    public async Task CloseAuctionAsync(string auctionId)
    {
        var auction = await _auctionRepository.GetAuctionByIdAsync(auctionId)
                      ?? throw new AuctionDoesNotExistExeption(auctionId);

        auction.Close();
        await _auctionRepository.UpdateAuctionAsync(auction);
    }

    public async Task CancelAuctionAsync(string auctionId)
    {
        var auction = await _auctionRepository.GetAuctionByIdAsync(auctionId) ??
                      throw new AuctionDoesNotExistExeption(auctionId);

        auction.Cancel();
        await _auctionRepository.UpdateAuctionAsync(auction);
    }

    public async Task PlaceBidAsync(string auctionId, PlaceBidDto placeBidDto)
    {
        if (placeBidDto == null)
        {
            throw new ArgumentNullException(nameof(placeBidDto));
        }

        var auction = await _auctionRepository.GetAuctionByIdAsync(auctionId) ??
                      throw new AuctionDoesNotExistExeption(auctionId);

        var bid = new Bid(placeBidDto.UserId, placeBidDto.Value);
        auction.PlaceBid(bid);

        await _auctionRepository.UpdateAuctionAsync(auction);
    }

    public async Task<IEnumerable<Auction>> GetAllOpenAuctionsAsync()
    {
        return await _auctionRepository.GetAllByFilterAsync(a => a.AuctionStatus == AuctionStatus.Active);
    }

    public async Task<Auction?> GetAuctionByIdAsync(string auctionId)
    {
        return await _auctionRepository.GetAuctionByIdAsync(auctionId);
    }
}