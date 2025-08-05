using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.Entities.Vehicle;

public class Sedan : Vehicle
{
    public override VehicleType Type => VehicleType.Sedan;
    public int NumberOfDoors { get; }
/**/
    public Sedan(string manufacturer, string model, int year, decimal startingBid, int numberOfDoors, string? id = null)
        : base(manufacturer, model, year, startingBid, id)
    {
        if (numberOfDoors != 2 && numberOfDoors != 4)
            throw new InvalidNumberOfDoorsException(nameof(VehicleType.Sedan), numberOfDoors);
        NumberOfDoors = numberOfDoors;
    }
}