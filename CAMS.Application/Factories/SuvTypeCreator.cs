using CAMS.Application.Dtos;
using CAMS.Application.Interfaces;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;

namespace CAMS.Application.Factories;

public class SuvCreator : IVehicleCreator
{
    public Vehicle Create(CreateVehicleDto createVehicleDto)
    {
        return new Suv(createVehicleDto.Manufacturer, createVehicleDto.Model, createVehicleDto.Year,
            createVehicleDto.StartingBid, createVehicleDto.NumberOfSeats!.Value, createVehicleDto.Id);
    }

    public VehicleType Type => VehicleType.Suv;
}