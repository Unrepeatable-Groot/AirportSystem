using AirportSystem.Application.Dtos;
using FluentValidation;

namespace AirportSystem.Application.Validators;

public class UsersCreateDtoValidator : AbstractValidator<UsersCreateDto>
{
    public UsersCreateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.PersonalId)
            .NotEmpty()
            .Length(11);

        RuleFor(x => x.Age)
            .GreaterThan(17)
            .When(x => x.Age.HasValue);

        RuleFor(x => x.Gender)
            .MaximumLength(32)
            .When(x => x.Gender != null);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{6,15}$")
            .When(x => x.PhoneNumber != null);
    }
}