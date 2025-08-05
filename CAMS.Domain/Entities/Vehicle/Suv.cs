using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.Entities.Vehicle;

public class Suv : Vehicle
{
    public override VehicleType Type => VehicleType.Suv;
    public int NumberOfSeats { get; }

    public Suv(string manufacturer, string model, int year, decimal startingBid, int numberOfSeats,
        string? id = null) : base(manufacturer, model, year, startingBid, id)
    {
        if (numberOfSeats < 5 || numberOfSeats > 9)
            throw new InvalidNumberOfSeatsException(nameof(VehicleType.Suv), numberOfSeats);
        NumberOfSeats = numberOfSeats;
    }
}