namespace CAMS.Domain.Exceptions;

public class InvalidVehicleYearException : ArgumentOutOfRangeException
{
    public InvalidVehicleYearException(int year)
        : base(nameof(year),
            $"The year '{year}' is not valid. Year must be between 1885 and less or equal than {DateTime.Now.Year}.")
    {
    }
}

public class InvalidVehicleStartingBidException : ArgumentException
{
    public InvalidVehicleStartingBidException()
        : base("The The starting bid must be greater than zero.")
    {
    }
}

public class InvalidNumberOfDoorsException : ArgumentOutOfRangeException
{
    public InvalidNumberOfDoorsException(string vehicleType, int actual)
        : base($"A {vehicleType} must have a valid number of doors. Provided: {actual}.")
    {
    }
}

public class InvalidNumberOfSeatsException : ArgumentOutOfRangeException
{
    public InvalidNumberOfSeatsException(string vehicleType, int actual)
        : base($"A {vehicleType} must have a valid number of seats. Provided: {actual}.")
    {
    }
}

public class InvalidLoadCapacityException : ArgumentOutOfRangeException
{
    public InvalidLoadCapacityException(double value)
        : base($"Invalid load capacity. Provided: {value}.")
    {
    }
}