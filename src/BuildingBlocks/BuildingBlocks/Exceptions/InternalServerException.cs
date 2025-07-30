namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message)
    {
        Details = string.Empty; // Initialize Details to avoid CS8618
    }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string Details { get; private set; }
}
