using Auth.Api.Validator;

namespace Auth.Api.Modules;

public class AuthModule:CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async ( RegistrationRequestDto model, IValidator<RegistrationRequestDto> validator, IAuthService authService) =>
        {
            //var authService = context.Items["AuthService"] as IAuthService;
            //var messageBus = context.Items["MessageBus"] as IMessageBus;
           // var configuration = context.RequestServices.GetRequiredService<IConfiguration>();

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
        
        app.MapPost("/auth/login", async (HttpContext context, LoginRequestDto model, IValidator<LoginRequestDto> validator,IAuthService authService) =>
        {
            //var authService = context.Items["AuthService"] as IAuthService;

            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var response = new ResponseDto(null, false, validationResult.ToString());
                return Results.BadRequest(response);
            }

            var (loginResponse, errorMessage) = await authService.Login(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                var response = new ResponseDto(null, false, "Username or password is incorrect");
                return Results.BadRequest(response);
            }

            var successResponse = new ResponseDto(loginResponse, true, null);
            return Results.Ok(successResponse);
        });

        app.MapPost("/auth/assignRole", async (HttpContext context, RegistrationRequestDto model, IValidator<RegistrationRequestDto> validator, IAuthService authService) =>
        {
           // var authService = context.Items["AuthService"] as IAuthService;

            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var response = new ResponseDto(null, false, validationResult.ToString());
                return Results.BadRequest(response);
            }

            var (isSuccessful, errorMessage) = await authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!isSuccessful)
            {
                var response = new ResponseDto(null, false, "Error encountered");
                return Results.BadRequest(response);
            }

            var successResponse = new ResponseDto(null, true, null);
            return Results.Ok(successResponse);
        });
    }
}