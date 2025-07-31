

namespace Auth.Domain.Interfaces;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string hashedpassword, string providedPassword);
}
