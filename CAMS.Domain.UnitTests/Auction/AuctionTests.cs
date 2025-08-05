using CAMS.Domain.Entities;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.UnitTests.Auction;

public class AuctionTests
{
    private readonly Vehicle _hatchback;
    private readonly Entities.Auction.Auction _auction;
    private readonly Bid _bid;

    public AuctionTests()
    {
        // Arrange
        _hatchback = new Hatchback(
            Faker.Company.Name(),
            Faker.Company.Suffix(),
            2025,
            (decimal)Faker.RandomNumber.Next(3, 4),
            3);

        _auction = new Entities.Auction.Auction(_hatchback);

        _bid = new Bid(Faker.Name.FullName(), 5);
    }

    [Fact]
    public void Start_WhenCreated_ShouldSetStatusToActive()
    {
        // Act
        _auction.Start();

        // Assert
        Assert.Equal(AuctionStatus.Active, _auction.AuctionStatus);
    }

    [Fact]
    public void Cancel_WhenCreated_ShouldSetStatusToCanceled()
    {
        _auction.Cancel();

        Assert.Equal(AuctionStatus.Canceled, _auction.AuctionStatus);
    }

    [Fact]
    public void Close_WhenActive_ShouldSetStatusToClosed()
    {
        _auction.Start();
        _auction.Close();

        Assert.Equal(AuctionStatus.Closed, _auction.AuctionStatus);
    }

    [Fact]
    public void CurrentBid_WhenAuctionIsActiveAndHasBids_ShouldReturnCurrentBid()
    {
        // Act
        _auction.Start();
        _auction.PlaceBid(_bid);

        // Assert
        Assert.True(_auction.CurrentBid != null);
    }

    [Fact]
    public void CurrentBid_WhenAuctionHasNoBids_ShouldReturnNull()
    {
        // Assert
        Assert.Null(_auction.CurrentBid);
    }

    #region ExceptionsTests

    [Fact]
    public void Start_WhenActive_ShouldThrowAuctionAlreadyStartedException()
    {
        // Act
        _auction.Start();

        // Assert
        Assert.Throws<AuctionAlreadyStartedException>(() => _auction.Start());
    }

    [Fact]
    public void Cancel_WhenCancelled_ShouldThrowAuctionCancelledException()
    {
        // Act
        _auction.Cancel();

        // Assert
        Assert.Throws<AuctionAlreadyCanceledException>(() => _auction.Cancel());
    }

    [Fact]
    public void Close_WhenClosed_ShouldThrowAuctionClosedException()
    {
        // Act
        _auction.Close();

        // Assert
        Assert.Throws<AuctionAlreadyClosedException>(() => _auction.Close());
    }

    [Fact]
    public void PlaceBid_WhenAuctionNotActive_ShouldThrowAuctionNotActiveException()
    {
        Assert.Throws<AuctionNotActiveException>(() => _auction.PlaceBid(_bid));
    }

    [Fact]
    void PlaceBid_WhenBidIsToLow_ShouldThrowAuctionBidIsTooLowException()
    {
        // Act
        _auction.Start();
        _auction.PlaceBid(_bid);

        // Assert
        Assert.Throws<BidTooLowException>(() => _auction.PlaceBid(_bid));
    }

    [Fact]
    void PlaceBid_WhenIsFirstBidAndIsTooLow_ShouldThrowAuctionBidIsTooLowException()
    {
        // Arrange
        var lowBid = new Bid(Faker.Name.FullName(), 2);

        // Act
        _auction.Start();

        // Assert
        Assert.Throws<BidTooLowException>(() => _auction.PlaceBid(lowBid));
    }

    #endregion
}