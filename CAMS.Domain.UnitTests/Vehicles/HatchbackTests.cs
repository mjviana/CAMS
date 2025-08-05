using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.UnitTests.Vehicles;

public class HatchbackTests
{
    [Fact]
    public void Hatchback_WhenCalled_SetsVehicleTypeToHatchback()
    {
        var hatchback = new Hatchback("foo", "foo", 1997, 3000m, 3);

        Assert.Equal(VehicleType.Hatchback, hatchback.Type);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    public void Hatchback_WithInvalidNumberOfDoors_ThrowsInvalidNumberOfDoorsException(int numberOfDoors)
    {
        Assert.Throws<InvalidNumberOfDoorsException>(() =>
            new Hatchback("foo", "foo", 1997, 3000m, numberOfDoors));
    }
}