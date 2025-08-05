using CAMS.Domain.Entities.Vehicle;
using CAMS.Domain.Enums;
using CAMS.Domain.Exceptions;

namespace CAMS.Domain.UnitTests.Vehicles;

public class SuvTests
{
    [Fact]
    public void Suv_WhenCalled_SetsVehicleTypeToSuv()
    {
        var suv = new Suv("foo", "foo", 1997, 3000m, 5);

        Assert.Equal(VehicleType.Suv, suv.Type);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(10)]
    public void Suv_WhithWrongNumberOfSeatsThrowsInvalidNumberOfSeatsException(int numberOfSeats)
    {
        Assert.Throws<InvalidNumberOfSeatsException>(() => new Suv("foo", "foo", 1997, 3000m, numberOfSeats));
    }
}