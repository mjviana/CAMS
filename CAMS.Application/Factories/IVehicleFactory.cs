using CAMS.Application.Dtos;
using CAMS.Domain.Entities.Vehicle;

namespace CAMS.Application.Factories;

public interface IVehicleFactory
{
    Vehicle Create(CreateVehicleDto createVehicleDto);
}