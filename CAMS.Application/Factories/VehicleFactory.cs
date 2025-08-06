using CAMS.Application.Dtos;
using CAMS.Application.Interfaces;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;

namespace CAMS.Application.Factories;

public class VehicleFactory : IVehicleFactory
{
    private readonly Dictionary<VehicleType, IVehicleTypeCreator> _vehicleCreators;

    public VehicleFactory(IEnumerable<IVehicleTypeCreator> vehicleCreators)
    {
        _vehicleCreators = vehicleCreators.ToDictionary(vc => vc.Type, vc => vc);
    }

    public Vehicle Create(CreateVehicleDto createVehicleDto)
    {
        if (!_vehicleCreators.TryGetValue(createVehicleDto.Type, out var creator))
        {
            throw new ArgumentException($"Unsupported vehicle type: {createVehicleDto.Type}");
        }

        return creator.Create(createVehicleDto);
    }
}