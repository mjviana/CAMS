using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.Entities.Auction;

public class Auction
{
    private readonly List<Bid> _bids = new();

    public string Id { get; }
    public Vehicle.Vehicle Vehicle { get; }
    public AuctionStatus AuctionStatus { get; private set; }
    public IReadOnlyList<Bid> Bids => _bids.AsReadOnly();
    public Bid CurrentBid => Bids.Any() ? Bids.OrderByDescending(b => b.Value).FirstOrDefault() : null;

    public Auction(Vehicle.Vehicle vehicle)
    {
        if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));

        Id = Guid.NewGuid().ToString();
        Vehicle = vehicle;
        AuctionStatus = AuctionStatus.Created;
    }

    public void Start()
    {
        if (AuctionStatus == AuctionStatus.Active) throw new AuctionAlreadyStartedException();
        AuctionStatus = AuctionStatus.Active;
    }

    public void Cancel()
    {
        if (AuctionStatus == AuctionStatus.Canceled) throw new AuctionAlreadyCanceledException();
        AuctionStatus = AuctionStatus.Canceled;
    }

    public void Close()
    {
        if (AuctionStatus == AuctionStatus.Closed) throw new AuctionAlreadyClosedException();
        AuctionStatus = AuctionStatus.Closed;
    }

    public void PlaceBid(Bid bid)
    {
        if (AuctionStatus != AuctionStatus.Active) throw new AuctionNotActiveException();
        if (CurrentBid == null)
        {
            if (bid.Value < Vehicle.StartingBid)
                throw new BidTooLowException();
        }
        else
        {
            if (bid.Value <= CurrentBid.Value)
                throw new BidTooLowException();
        }

        _bids.Add(bid);
    }
}