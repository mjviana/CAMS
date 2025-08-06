using System.Linq.Expressions;
using CAMS.Application.Dtos;
using CAMS.Application.Factories;
using CAMS.Application.Interfaces;
using CAMS.Application.Services;
using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;
using Moq;

namespace CAMS.Application.UnitTests;

public class VehicleServiceTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<IVehicleFactory> _vehicleFactoryMock;
    private readonly VehicleService _sut;

    public VehicleServiceTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _vehicleFactoryMock = new Mock<IVehicleFactory>();
        _sut = new VehicleService(_vehicleRepositoryMock.Object, _vehicleFactoryMock.Object);
    }

    [Fact]
    public async Task CreateVehicleAsync_WithValidData_ShouldCreateHatchback()
    {
        // Arrange
        var dto = new CreateVehicleDto(
            VehicleType.Hatchback,
            Faker.Company.Name(),
            Faker.Company.Suffix(),
            2000,
            1000m,
            3);

        _vehicleRepositoryMock.Setup(repo => repo.GetVehicleByIdAsync(dto.Id))
            .ReturnsAsync((Vehicle)null);

        _vehicleFactoryMock.Setup(f => f.Create(It.IsAny<CreateVehicleDto>()))
            .Returns(new Hatchback(dto.Manufacturer, dto.Model, dto.Year, dto.StartingBid, dto.NumberOfDoors!.Value));

        // Act
        await _sut.CreateVehicleAsync(dto);

        // Assert
        _vehicleRepositoryMock.Verify(repo => repo.AddVehicleAsync(It.IsAny<Hatchback>()), Times.Once);
    }

    [Fact]
    public async Task CreateVehicleAsync_WithADuplicatedIdentifier_ShouldThrowDuplicateIdentifierException()
    {
        // Arrange 
        var existingVehicle = new Hatchback(
            Faker.Company.Name(),
            Faker.Company.Suffix(),
            2000,
            1000m,
            3,
            "01");

        _vehicleRepositoryMock.Setup(repo => repo.GetVehicleByIdAsync(existingVehicle.Id))
            .ReturnsAsync(existingVehicle);

        _vehicleFactoryMock.Setup(f => f.Create(It.IsAny<CreateVehicleDto>()))
            .Returns(existingVehicle);

        var dto = new CreateVehicleDto(
            VehicleType.Hatchback,
            Faker.Company.Name(),
            Faker.Company.Suffix(),
            2000,
            1000m,
            3,
            Id: "01");

        // Act & Assert
        await Assert.ThrowsAsync<DuplicateIdentifierException>(async () => await _sut.CreateVehicleAsync(dto));
    }

    [Fact]
    public async Task CreateVehicleAsync_WithInvalidVehicleType_ShouldThrowArgumentException()
    {
        var dto = new CreateVehicleDto(
            (VehicleType)22,
            Faker.Company.Name(),
            Faker.Company.Suffix(),
            2000,
            1000m,
            3);

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CreateVehicleAsync(dto));
    }

    [Fact]
    public async Task CreateVehicleAsync_WithMissingNumberOfDoors_ShouldThrowArgumentException()
    {
        var dto = new CreateVehicleDto(
            VehicleType.Sedan,
            Faker.Company.Name(),
            Faker.Company.Suffix(),
            2000,
            1000m,
            null);

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CreateVehicleAsync(dto));
    }

    [Fact]
    public async Task GetVehicleByIdAsync_WithValidData_ShouldReturnVehicle()
    {
        // Arrange 
        var expectedVehicle = new Sedan("foo", "bar", 2000, 1999m, 2);

        _vehicleRepositoryMock.Setup(repo => repo.GetVehicleByIdAsync(expectedVehicle.Id))
            .ReturnsAsync(expectedVehicle);

        // Act
        var actualVehicle = await _sut.GetVehicleByIdAsync(expectedVehicle.Id);

        // Assert 
        Assert.Equal(expectedVehicle, actualVehicle);
    }

    [Fact]
    public async Task GetVehiclesByFilterAsync_WithValidData_ShouldReturnFilteredVehicles()
    {
        // Arrange
        var vehicles = new List<Vehicle>
        {
            new Suv("foo", "bar", 2000, 1999m, 5),
            new Sedan("foo", "bar", 2000, 1999m, 2),
            new Sedan("foo", "bar", 1999, 1999m, 2),
        };

        _vehicleRepositoryMock.Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<Vehicle, bool>>>()))
            .ReturnsAsync((Expression<Func<Vehicle, bool>> filter) =>
                vehicles.AsQueryable().Where(filter).ToList());

        // Act
        var result = await _sut.GetVehiclesByFilterAsync(v => v.Year > 1999);

        // Assert 
        Assert.Equal(2, result.Count());
        Assert.All(result, v => Assert.True(v.Year > 1999));
    }
}