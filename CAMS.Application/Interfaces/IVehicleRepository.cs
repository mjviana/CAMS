using System.Linq.Expressions;
using CAMS.Domain.Entities.Vehicle;

namespace CAMS.Application.Interfaces;

/// <summary>
/// Defines data access operations for managing vehicles in the auction inventory.
/// </summary>
public interface IVehicleRepository
{
    /// <summary>
    /// Adds a new vehicle to the repository.
    /// </summary>
    /// <param name="vehicle">The vehicle entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddVehicleAsync(Vehicle vehicle);

    /// <summary>
    /// Retrieves all vehicles in the repository.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The result contains a collection of all vehicles.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetAllVehicles();

    /// <summary>
    /// Retrieves all vehicles that match the specified filter criteria.
    /// </summary>
    /// <param name="filter">An expression used to filter vehicles based on specific conditions.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The result contains a collection of vehicles that satisfy the filter.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetAllByFilterAsync(Expression<Func<Vehicle, bool>> filter);

    /// <summary>
    /// Retrieves a specific vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The result contains the vehicle if found; otherwise, null.
    /// </returns>
    Task<Vehicle> GetVehicleByIdAsync(string id);
}