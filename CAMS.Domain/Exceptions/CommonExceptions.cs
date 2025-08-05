namespace CAMS.Domain.Exceptions;

public class DuplicateIdentifierException : ArgumentException
{
    public DuplicateIdentifierException()
        : base("Duplicate identifier.")
    {
    }
}