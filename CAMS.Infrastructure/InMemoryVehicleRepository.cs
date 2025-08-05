using System.Linq.Expressions;
using CAMS.Application.Interfaces;
using CAMS.Domain.Entities.Vehicle;

namespace CAMS.Infrastructure;

public class InMemoryVehicleRepository : IVehicleRepository
{
    private readonly List<Vehicle> _vehicles = new();

    public Task AddVehicleAsync(Vehicle vehicle)
    {
        _vehicles.Add(vehicle);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Vehicle>> GetAllVehicles()
    {
        var vehicles = _vehicles;
        return Task.FromResult(vehicles.AsEnumerable());
    }

    public Task<IEnumerable<Vehicle>> GetAllByFilterAsync(Expression<Func<Vehicle, bool>> filter)
    {
        var result = _vehicles.AsQueryable().Where(filter);
        return Task.FromResult(result.AsEnumerable());
    }

    public Task<Vehicle?> GetVehicleByIdAsync(string id)
    {
        var vehicle = _vehicles.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(vehicle);
    }
}