using CAMS.Application.Dtos;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;

namespace CAMS.Application.Factories;

public interface IVehicleCreator
{
    Vehicle Create(CreateVehicleDto createVehicleDto);
    VehicleType Type { get; }
}