using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.Entities.Vehicle;

public class Truck : Vehicle
{
    public override VehicleType Type => VehicleType.Truck;
    public double LoadCapacity { get; }

    public Truck(string manufacturer, string model, int year, decimal startingBid, double loadCapacity,
        string? id = null) : base(manufacturer, model, year, startingBid, id)
    {
        if (loadCapacity <= 0) throw new InvalidLoadCapacityException(loadCapacity);
        LoadCapacity = loadCapacity;
    }
}