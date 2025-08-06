using CAMS.Application.Dtos;
using CAMS.Application.Interfaces;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;

namespace CAMS.Application.Factories;

public class TruckTypeCreator : IVehicleTypeCreator
{
    public Vehicle Create(CreateVehicleDto createVehicleDto)
    {
        return new Truck(createVehicleDto.Manufacturer, createVehicleDto.Model, createVehicleDto.Year,
            createVehicleDto.StartingBid, createVehicleDto.LoadCapacity!.Value, createVehicleDto.Id);
    }

    public VehicleType Type => VehicleType.Truck;
}