using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.UnitTests.Vehicles;

public class TruckTests
{
    [Fact]
    public void Truck_WhenCalled_SetsVehicleTypeToTruck()
    {
        var truck = new Truck("foo", "foo", 1997, 3000m, 5);

        Assert.Equal(VehicleType.Truck, truck.Type);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Truck_WithInvalidLoadCapacityThrowsInvalidLoadCapacityException(double loadCapacity)
    {
        Assert.Throws<InvalidLoadCapacityException>(() => new Truck("foo", "foo", 1997, 3000m, loadCapacity));
    }
}