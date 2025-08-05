using System.Linq.Expressions;
using CAMS.Application.Dtos;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;

namespace CAMS.Application.Interfaces;

/// <summary>
/// Defines the contract for vehicle-related business operations in the Car Auction Management System.
/// </summary>
public interface IVehicleService
{
    /// <summary>
    /// Creates and adds a new vehicle to the auction inventory.
    /// </summary>
    /// <param name="createVehicleDto">The data transfer object containing vehicle details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateVehicleAsync(CreateVehicleDto createVehicleDto);

    /// <summary>
    /// Retrieves all vehicles currently in the inventory.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The result contains a collection of all vehicles.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

    /// <summary>
    /// Retrieves vehicles that match the specified manufacturer.
    /// </summary>
    /// <param name="manufacturer">The manufacturer to filter by.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The result contains a collection of vehicles from the specified manufacturer.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetVehiclesByManufacturer(string manufacturer);

    /// <summary>
    /// Retrieves vehicles of a specific type (e.g., Sedan, SUV, Truck).
    /// </summary>
    /// <param name="vehicleType">The type of vehicle to filter by.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The result contains a collection of vehicles of the specified type.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetVehiclesByType(VehicleType vehicleType);

    /// <summary>
    /// Retrieves vehicles that match the specified model.
    /// </summary>
    /// <param name="model">The model name to filter by.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The result contains a collection of vehicles with the specified model name.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetVehiclesByModel(string model);

    /// <summary>
    /// Retrieves vehicles that were manufactured in the specified year.
    /// </summary>
    /// <param name="year">The year of manufacture to filter by.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The result contains a collection of vehicles from the specified year.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetVehiclesByYear(int year);

    /// <summary>
    /// Retrieves a single vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The result contains the vehicle if found; otherwise, null.
    /// </returns>
    Task<Vehicle?> GetVehicleByIdAsync(string id);

    /// <summary>
    /// Retrieves vehicles that match a custom filter expression.
    /// </summary>
    /// <param name="filter">An expression used to filter vehicles based on custom criteria.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The result contains a collection of vehicles that match the filter.
    /// </returns>
    Task<IEnumerable<Vehicle>> GetVehiclesByFilterAsync(Expression<Func<Vehicle, bool>> filter);
}