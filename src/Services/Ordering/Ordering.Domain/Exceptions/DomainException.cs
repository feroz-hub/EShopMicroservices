namespace Ordering.Domain.Exceptions;

public class DomainException(string message) : Exception($"Domain Exception: \"{message}\"throw from Domain Layer.");