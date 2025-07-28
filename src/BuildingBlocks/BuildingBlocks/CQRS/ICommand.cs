namespace BuildingBlocks.CQRS;

/// <summary>
/// Marker interface for a command that does not return a value.
/// </summary>
public interface ICommand : ICommand<Unit>
{
}

/// <summary>
/// Represents a command with a response type.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}