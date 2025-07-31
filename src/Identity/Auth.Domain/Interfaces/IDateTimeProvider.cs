namespace Auth.Domain.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
