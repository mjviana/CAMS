namespace CAMS.Domain.Exceptions;

public class AuctionAlreadyStartedException : InvalidOperationException
{
    public AuctionAlreadyStartedException() : base("Auction has already been started.")
    {
    }
}

public class AuctionAlreadyCanceledException : InvalidOperationException
{
    public AuctionAlreadyCanceledException() : base("Auction has already been canceled.")
    {
    }
}

public class AuctionAlreadyClosedException : InvalidOperationException
{
    public AuctionAlreadyClosedException() : base("Auction has already been closed.")
    {
    }
}

public class AuctionNotActiveException : InvalidOperationException
{
    public AuctionNotActiveException() : base("Auction is not active.")
    {
    }
}

public class BidTooLowException : InvalidOperationException
{
    public BidTooLowException() : base("Bid amount must be greater than the current bid.")
    {
    }
}

public class AuctionDoesNotExistExeption : InvalidOperationException
{
    public AuctionDoesNotExistExeption(string id) : base($"Auction with id:{id} does not exist.")
    {
    }
}

public class VehicleDoesNotExistInAuctionInventoryException : InvalidOperationException
{
    public VehicleDoesNotExistInAuctionInventoryException(string id) : base(
        $"Vehicle with id:{id} does not exist in auction inventory.")
    {
    }
}

public class VehicleAlreadyAuctionedException : InvalidOperationException
{
    public VehicleAlreadyAuctionedException(string id) : base(
        $"Vehicle with id:{id} is already associated with an auction.")
    {
    }
}