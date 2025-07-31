

namespace Auth.Domain.Interfaces;

internal interface IJwtProvider
{
    string GenerateToken(string userId, string email, string role, DateTime? expiresAt = null);
    string GenerateRefreshToken(string userId, string email, DateTime? expiresAt = null);
    bool ValidateToken(string token);
    (string UserId, string Email, string Role) GetTokenClaims(string token);
    (string UserId, string Email) GetRefreshTokenClaims(string token);
}
