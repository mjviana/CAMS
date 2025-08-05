using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.Entities.Vehicle;

public abstract class Vehicle
{
    public string Id { get; }
    public string Manufacturer { get; }
    public string Model { get; }
    public int Year { get; }
    public decimal StartingBid { get; }

    public abstract VehicleType Type { get; }

    protected Vehicle(string manufacturer, string model, int year, decimal startingBid, string? id = null)
    {
        if (string.IsNullOrWhiteSpace(manufacturer))
            throw new ArgumentException("Manufacturer cannot be empty.", nameof(manufacturer));

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty.", nameof(model));

        if (year <= 1885 || year > DateTime.Now.Year)
            throw new InvalidVehicleYearException(year);

        if (startingBid <= 0)
            throw new InvalidVehicleStartingBidException();

        Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        StartingBid = startingBid;
    }
}