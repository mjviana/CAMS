using System.Linq.Expressions;
using CAMS.Application.Dtos;
using CAMS.Application.Factories;
using CAMS.Application.Interfaces;
using CAMS.Application.Validators;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IVehicleFactory _vehicleFactory;

    public VehicleService(IVehicleRepository vehicleRepository, IVehicleFactory vehicleFactory)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleFactory = vehicleFactory;
    }

    public async Task CreateVehicleAsync(CreateVehicleDto createVehicleDto)
    {
        CreateVehicleDtoValidator.Validate(createVehicleDto);

        var vehicle = _vehicleFactory.Create(createVehicleDto);

        if (await _vehicleRepository.GetVehicleByIdAsync(vehicle.Id) != null)
        {
            throw new DuplicateIdentifierException();
        }

        await _vehicleRepository.AddVehicleAsync(vehicle);
    }

    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return await _vehicleRepository.GetAllVehicles();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByManufacturer(string manufacturer)
    {
        return await _vehicleRepository.GetAllByFilterAsync(v => v.Manufacturer == manufacturer);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByType(VehicleType vehicleType)
    {
        return await _vehicleRepository.GetAllByFilterAsync(v => v.Type == vehicleType);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByModel(string model)
    {
        return await _vehicleRepository.GetAllByFilterAsync(v => v.Model == model);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByYear(int year)
    {
        return await _vehicleRepository.GetAllByFilterAsync(v => v.Year == year);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByFilterAsync(Expression<Func<Vehicle, bool>> filter)
    {
        return await _vehicleRepository.GetAllByFilterAsync(filter);
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(string id)
    {
        return await _vehicleRepository.GetVehicleByIdAsync(id);
    }
}