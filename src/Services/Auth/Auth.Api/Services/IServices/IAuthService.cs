namespace Auth.Api.Services.IServices;

public interface IAuthService
{
    Task<(UserDto User, string ErrorMessage)> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
}