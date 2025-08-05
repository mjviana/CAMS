using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.Entities.Vehicle;

public class Hatchback : Vehicle
{
    public override VehicleType Type => VehicleType.Hatchback;
    public int NumberOfDoors { get; }

    public Hatchback(string manufacturer, string model, int year, decimal startingBid, int numberOfDoors,
        string? id = null) : base(manufacturer, model, year, startingBid, id)
    {
        if (numberOfDoors != 3 && numberOfDoors != 5)
            throw new InvalidNumberOfDoorsException(nameof(VehicleType.Hatchback), numberOfDoors);
        NumberOfDoors = numberOfDoors;
    }
}