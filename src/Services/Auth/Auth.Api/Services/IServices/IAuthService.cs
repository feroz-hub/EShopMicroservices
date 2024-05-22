namespace Auth.Api.Services.IServices;

public interface IAuthService
{
    Task<(UserDto User, string ErrorMessage)> Register(RegistrationRequestDto registrationRequestDto);
    Task<(LoginResponseDto loginResponseDto,string ErrorMessage)> Login(LoginRequestDto loginRequestDto);
    Task<(bool IsSuccess,string ErrorMessage)> AssignRole(string email, string roleName);
}