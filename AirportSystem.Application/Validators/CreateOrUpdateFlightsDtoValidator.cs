using AirportSystem.Application.Dtos;
using FluentValidation;

namespace AirportSystem.Application.Validators;

public class CreateOrUpdateFlightsDtoValidator : AbstractValidator<CreateOrUpdateFlightsDto>
{
    public CreateOrUpdateFlightsDtoValidator()
    {
        RuleFor(x => x.From)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(x => x.Destination)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(x => x.SeatCount)
            .GreaterThan(0);

        RuleFor(x => x.DepartureTime)
            .LessThan(x => x.ArrivalTime)
            .WithMessage("DepartureTime must be earlier than ArrivalTime");
    }
}