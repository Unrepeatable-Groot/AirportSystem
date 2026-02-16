using AirportSystem.Application.Dtos;
using FluentValidation;

namespace AirportSystem.Application.Validators;

public class ChangeEmailDtoValidator : AbstractValidator<ChangeEmailDto>
{
    public ChangeEmailDtoValidator()
    {
        RuleFor(x => x.NewEmail)
            .NotEmpty()
            .EmailAddress();
    }
}