namespace Auth.Api.Validator;

public class RegistrationRequestDtoValidator:AbstractValidator<RegistrationRequestDto>
{
    public RegistrationRequestDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
        RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required.");
    }
}