

using Auth.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Auth.Domain.ValueObjects;


public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidEmailException("Email is required.");

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new InvalidEmailException("Invalid email format.");

        Value = value;
    }

    public override string ToString() => Value;
}