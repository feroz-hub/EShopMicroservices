using Auth.Api.Validator;

namespace Auth.Api.Modules;

public class AuthModule(IAuthService authService,IJwtTokenGenerator jwtTokenGenerator):CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (HttpRequest request, HttpResponse res) =>
        {
            var user = await request.ReadFromJsonAsync<RegistrationRequestDto>();
            var validator = new RegistrationRequestDtoValidator();
            var validationResult = validator.ValidateAsync(user);
            if (!validationResult.Result.IsValid)
            {
                res.StatusCode = StatusCodes.Status400BadRequest;
                await res.WriteAsJsonAsync(new ResponseDto(null, false, validationResult.ToString()));
                return null;
            }
            
            var errorMessage = await authService.Register(user);
            if (string.IsNullOrEmpty(errorMessage)) return TypedResults.Created($"/auth/register/{user.Name}", errorMessage);
            res.StatusCode = StatusCodes.Status400BadRequest;
            await res.WriteAsJsonAsync(new ResponseDto(null, false, errorMessage));
            return null;
        });
    }
}