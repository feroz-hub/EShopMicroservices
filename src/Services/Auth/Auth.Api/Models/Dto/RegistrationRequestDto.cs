namespace Auth.Api.Models.Dto;

public record RegistrationRequestDto(string Email,string Name,string Password,string PhoneNumber,string Role);