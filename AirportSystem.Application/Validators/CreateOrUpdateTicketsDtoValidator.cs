using AirportSystem.Application.Dtos;
using FluentValidation;

namespace AirportSystem.Application.Validators;

public class CreateOrUpdateTicketsDtoValidator : AbstractValidator<CreateOrUpdateTicketsDto>
{
    public CreateOrUpdateTicketsDtoValidator()
    {
        RuleFor(x => x.SeatNumber)
            .GreaterThan(0);

        RuleFor(x => x.FlightId)
            .GreaterThan(0);

        RuleFor(x => x.PassengerId)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}