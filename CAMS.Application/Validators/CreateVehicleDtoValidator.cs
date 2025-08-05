using CAMS.Application.Dtos;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Application.Validators;

public static class CreateVehicleDtoValidator
{
    public static void Validate(CreateVehicleDto createVehicleDto)
    {
        ArgumentNullException.ThrowIfNull(createVehicleDto);

        if (string.IsNullOrWhiteSpace(createVehicleDto.Manufacturer))
            throw new ArgumentException("Manufacturer is required.", nameof(createVehicleDto.Manufacturer));

        if (string.IsNullOrWhiteSpace(createVehicleDto.Model))
            throw new ArgumentException("Model is required.", nameof(createVehicleDto.Model));

        if (createVehicleDto.Year <= 1885 || createVehicleDto.Year > DateTime.Now.Year)
            throw new InvalidVehicleYearException(createVehicleDto.Year);

        if (createVehicleDto.StartingBid <= 0)
            throw new InvalidVehicleStartingBidException();

        switch (createVehicleDto.Type)
        {
            case VehicleType.Hatchback:
            case VehicleType.Sedan:
                if (!createVehicleDto.NumberOfDoors.HasValue)
                    throw new ArgumentException("Number of doors is required for hatchbacks and sedans.");
                break;
            case VehicleType.Suv:
                if (!createVehicleDto.NumberOfSeats.HasValue)
                    throw new ArgumentException("Number of seats is required for SUVs.");
                break;
            case VehicleType.Truck:
                if (!createVehicleDto.LoadCapacity.HasValue)
                    throw new ArgumentException("Load capacity is required for trucks.");
                break;
            default:
                throw new ArgumentException($"Unsupported vehicle type: {createVehicleDto.Type}");
        }
    }
}