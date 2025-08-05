using System.Linq.Expressions;
using CAMS.Application.Dtos;
using CAMS.Application.Interfaces;
using CAMS.Application.Validators;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task CreateVehicleAsync(CreateVehicleDto createVehicleDto)
    {
        CreateVehicleDtoValidator.Validate(createVehicleDto);

        Vehicle vehicle = createVehicleDto.Type switch
        {
            VehicleType.Hatchback => new Hatchback(createVehicleDto.Model, createVehicleDto.Model,
                createVehicleDto.Year, createVehicleDto.StartingBid, createVehicleDto.NumberOfDoors.Value,
                createVehicleDto.Id),
            VehicleType.Sedan => new Sedan(createVehicleDto.Manufacturer, createVehicleDto.Model,
                createVehicleDto.Year, createVehicleDto.StartingBid, createVehicleDto.NumberOfDoors.Value,
                createVehicleDto.Id),
            VehicleType.Suv => new Suv(createVehicleDto.Manufacturer, createVehicleDto.Model,
                createVehicleDto.Year,
                createVehicleDto.StartingBid, createVehicleDto.NumberOfSeats!.Value, createVehicleDto.Id),
            VehicleType.Truck => new Truck(createVehicleDto.Manufacturer, createVehicleDto.Model,
                createVehicleDto.Year, createVehicleDto.StartingBid, createVehicleDto.LoadCapacity!.Value,
                createVehicleDto.Id),
            _ => throw new ArgumentException("Invalid vehicle type")
        };

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