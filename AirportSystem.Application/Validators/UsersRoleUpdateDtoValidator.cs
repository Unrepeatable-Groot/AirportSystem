using AirportSystem.Application.Dtos;
using FluentValidation;

namespace AirportSystem.Application.Validators;

public class UsersRoleUpdateDtoValidator : AbstractValidator<UsersRoleUpdateDto>
{
    public UsersRoleUpdateDtoValidator()
    {
        RuleFor(x => x.Role)
            .NotNull()
            .IsInEnum();
    }
}