namespace Auth.Api.Models.Dto;

public record LoginResponseDto(UserDto UserDto, string Token);