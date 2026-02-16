using AirportSystem.Application.Dtos;
using FluentValidation;

namespace AirportSystem.Application.Validators;

public class UsersUpdateDtoValidator : AbstractValidator<UsersUpdateDto>
{
    public UsersUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(128)
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .MaximumLength(128)
            .When(x => x.LastName != null);

        RuleFor(x => x.Age)
            .GreaterThan(0)
            .When(x => x.Age.HasValue);

        RuleFor(x => x.Gender)
            .MaximumLength(32)
            .When(x => x.Gender != null);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{6,15}$")
            .When(x => x.PhoneNumber != null);
    }
}