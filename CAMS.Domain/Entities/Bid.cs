namespace CAMS.Domain.Entities;

public class Bid
{
    public string Id { get; }
    public string UserId { get; }
    public decimal Value { get; }

    public Bid(string userId, decimal value)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User Id cannot be empty.", nameof(userId));
        if (value <= 0)
            throw new ArgumentException("Value must be greater than zero.", nameof(value));

        Id = Guid.NewGuid().ToString();
        UserId = userId;
        Value = value;
    }
}