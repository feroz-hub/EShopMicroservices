

namespace Auth.Domain.ValueObjects;

public record Password
{
    public string HashedValue { get; }

    public Password(string hashedValue)
    {
        if (string.IsNullOrWhiteSpace(hashedValue))
            throw new ArgumentException("Password hash cannot be empty.");

        HashedValue = hashedValue;
    }

    public override string ToString() => HashedValue;
}
