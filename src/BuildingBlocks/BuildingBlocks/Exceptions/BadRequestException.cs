namespace BuildingBlocks.Exceptions;

public class BadRequestException:Exception
{
    public BadRequestException(string message) : base(message)
    {
        Details = string.Empty; // Initialize Details to avoid CS8618
    }

    public BadRequestException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string Details { get; private set; }
    
}