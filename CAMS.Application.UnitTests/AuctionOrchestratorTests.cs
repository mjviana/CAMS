using System.Linq.Expressions;
using CAMS.Application.Dtos;
using CAMS.Application.Interfaces;
using CAMS.Application.Services;
using CAMS.Domain.Entities.Auction;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;
using Moq;

namespace CAMS.Application.UnitTests;

public class AuctionOrchestratorTests
{
    private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly AuctionOrchestrator _sut;
    private readonly Sedan _sedan;
    private readonly Auction _auction;

    public AuctionOrchestratorTests()
    {
        _auctionRepositoryMock = new Mock<IAuctionRepository>();
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _sut = new AuctionOrchestrator(_vehicleRepositoryMock.Object, _auctionRepositoryMock.Object);
        _sedan = new Sedan("foo", "bar", 2000, 1000, 2);
        _auction = new Auction(_sedan);
    }

    [Fact]
    public async Task StartAuctionAsync_WhenVehicleNotInOtherAuction_ShouldCreateAuction()
    {
        // Arrange
        _vehicleRepositoryMock
            .Setup(repo => repo.GetVehicleByIdAsync(_sedan.Id))
            .ReturnsAsync(_sedan);

        _auctionRepositoryMock
            .Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<Auction, bool>>>()))
            .ReturnsAsync(new List<Auction>());

        // Act
        await _sut.StartAuctionAsync(_sedan.Id);

        // Assert
        _auctionRepositoryMock.Verify(r => r.AddAuctionAsync(
            It.Is<Auction>(a => a.Vehicle.Id == _sedan.Id && a.AuctionStatus == AuctionStatus.Active)), Times.Once);
    }

    [Fact]
    public async Task
        StartAuctionAsync_WhenVehicleIsNotInAuctionInventory_ShouldThrowVehicleDoesNotExistInAuctionInventoryException()
    {
        // Arrange
        _vehicleRepositoryMock
            .Setup(repo => repo.GetVehicleByIdAsync(_sedan.Id))
            .ReturnsAsync((Vehicle)null);

        // Act and Assert
        await Assert.ThrowsAsync<VehicleDoesNotExistInAuctionInventoryException>(() =>
            _sut.StartAuctionAsync(_sedan.Id));
    }

    [Fact]
    public async Task StartAuctionAsync_WhenVehicleIsInOtherActiveAuction_ShouldThrowVehicleInActiveAuctionException()
    {
        // Arrange
        _auction.Start(); // sets the AuctionStatus to Active

        _vehicleRepositoryMock
            .Setup(repo => repo.GetVehicleByIdAsync(_sedan.Id))
            .ReturnsAsync(_sedan);

        _auctionRepositoryMock
            .Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<Auction, bool>>>()))
            .ReturnsAsync(new List<Auction> { _auction });

        // Act & Assert
        await Assert.ThrowsAsync<VehicleAlreadyAuctionedException>(() =>
            _sut.StartAuctionAsync(_sedan.Id));
    }

    [Fact]
    public async Task CloseAuctionAsync_WhenAuctionIsActive_ShouldCloseAuction()
    {
        // Arrange
        _auction.Start();

        _auctionRepositoryMock
            .Setup(repo => repo.GetAuctionByIdAsync(_auction.Id))
            .ReturnsAsync(_auction);

        await _sut.CloseAuctionAsync(_auction.Id);

        Assert.True(_auction.AuctionStatus == AuctionStatus.Closed);
        _auctionRepositoryMock.Verify(repo => repo.UpdateAuctionAsync(_auction), Times.Once);
    }

    [Fact]
    public async Task CloseAuctionAsync_WhenAuctionDoesNotExist_ShouldThrowAuctionDoesNotExistException()
    {
        // Arrange
        _auctionRepositoryMock
            .Setup(repo => repo.GetAuctionByIdAsync(_auction.Id))
            .ReturnsAsync((Auction)null);

        // Act & Asser
        await Assert.ThrowsAsync<AuctionDoesNotExistExeption>(() => _sut.CloseAuctionAsync(_auction.Id));
    }

    [Fact]
    public async Task PlaceBidAsync_WhenIsValid_ShouldSetNewAuctionCurrentBid()
    {
        // Arrange
        var placeBidDto = new PlaceBidDto("01", 1001);
        _auction.Start();

        _auctionRepositoryMock.Setup(repo => repo.GetAuctionByIdAsync(_auction.Id)).ReturnsAsync(_auction);

        // Act
        await _sut.PlaceBidAsync(_auction.Id, placeBidDto);

        // Assert
        Assert.True(_auction.CurrentBid.Value == placeBidDto.Value);
        Assert.True(_auction.CurrentBid.UserId == placeBidDto.UserId);
        _auctionRepositoryMock.Verify(repo => repo.UpdateAuctionAsync(_auction), Times.Once);
    }

    [Fact]
    public async Task PlaceBidAsync_WhenIsNull_ShouldThrowArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.PlaceBidAsync(_auction.Id, null));
    }
}