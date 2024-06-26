namespace Auth.Api.Models;

public class JwtOptions
{
    public string Issuer { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public string Secret { get; set; } = default!;
}