using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.UnitTests.Vehicles;

public class BaseVehicleTests
{
    [Fact]
    public void Constructor_WithValidProperties_CreatesHatchback()
    {
        // Arrange
        var hatchback = new Hatchback("Volkswagen", "Golf", 1997, 3000m, 3);

        // Assert
        Assert.Equal("Volkswagen", hatchback.Manufacturer);
        Assert.Equal("Golf", hatchback.Model);
        Assert.Equal(1997, hatchback.Year);
        Assert.Equal(3000m, hatchback.StartingBid);
        Assert.Equal(3, hatchback.NumberOfDoors);
    }

    [Fact]
    public void Constructor_WithInvalidManufacturer_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Hatchback("", "Golf", 1997, 3000m, 3));
    }

    [Fact]
    public void Constructor_WithInvalidModel_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Hatchback("Volkswagen", "", 1997, 3000m, 3));
    }

    [Theory]
    [InlineData(1885)]
    [InlineData(1884)]
    [InlineData(3099)]
    public void Constructor_WithInvalidYear_ShouldThrowInvalidYearException(int year)
    {
        Assert.Throws<InvalidVehicleYearException>(() => new Hatchback("Volkswagen", "Golf", year, 3000m, 3));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Constructor_WithInvalidStartingBid_ShouldThrowInvalidStartingBidException(decimal startingBid)
    {
        Assert.Throws<InvalidVehicleStartingBidException>(() =>
            new Hatchback("Volkswagen", "Golf", 1997, startingBid, 3));
    }
}