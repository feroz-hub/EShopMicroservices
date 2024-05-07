namespace BuildingBlocks.Exceptions;

public class NotFountException:Exception
{
    public NotFountException(string message):base(message){}
    protected NotFountException(string name, object key) : base($"Entity \"{name}\"({key}) was not fount") {}
}