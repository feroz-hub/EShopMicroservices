using Auth.Api.Validator;

namespace Auth.Api.Modules;

public class AuthModule:CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register", async (HttpContext context, RegistrationRequestDto model, IValidator<RegistrationRequestDto> validator) =>
        {
            var authService = context.Items["AuthService"] as IAuthService;
            //var messageBus = context.Items["MessageBus"] as IMessageBus;
            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();

            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var response = new ResponseDto(null, false, validationResult.ToString());
                return Results.BadRequest(response);
            }

            var (user, errorMessage) = await authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                var response = new ResponseDto(null, false, errorMessage);
                return Results.BadRequest(response);
            }

            //await messageBus.PublishMessage(model.Email, configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));
            var successResponse = new ResponseDto(user, true, null);
            return Results.Ok(successResponse);
        });
    }
}