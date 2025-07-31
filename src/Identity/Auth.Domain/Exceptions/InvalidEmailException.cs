

using BuildingBlocks.Exceptions;


namespace Auth.Domain.Exceptions;

internal class InvalidEmailException(string message):AuthException(message)
{
}
