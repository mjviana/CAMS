using CAMS.Domain.Enums;

namespace CAMS.Application.Dtos;

public record CreateVehicleDto(
    VehicleType Type,
    string Manufacturer,
    string Model,
    int Year,
    decimal StartingBid,
    int? NumberOfDoors = null,
    int? NumberOfSeats = null,
    double? LoadCapacity = null,
    string? Id = null);