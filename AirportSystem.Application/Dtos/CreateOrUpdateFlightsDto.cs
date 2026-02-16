using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Dtos;

public class CreateOrUpdateFlightsDto
{
    public string From { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public int SeatCount { get; set; }

    public FlightStatus Status { get; set; } = FlightStatus.Scheduled;
}
