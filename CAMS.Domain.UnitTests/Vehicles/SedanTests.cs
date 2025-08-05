using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.UnitTests.Vehicles;

public class SedanTests
{
    [Fact]
    public void Sedan_WhenCalled_SetsVehicleTypeToSedan()
    {
        var sedan = new Sedan("foo", "foo", 1997, 3000m, 4);

        Assert.Equal(VehicleType.Sedan, sedan.Type);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void Sedan_WithInvalidNumberOfDoors_ThrowsInvalidNumberOfDoorsException(int numberOfDoors)
    {
        Assert.Throws<InvalidNumberOfDoorsException>(() => new Sedan("foo", "foo", 2000, 2000m, numberOfDoors));
    }
}