using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Dtos;

public class FlightsDto
{
    public FlightsDto(Flights flights) 
    { 
        Id = flights.Id;
        From = flights.From;
        Destination = flights.Destination;
        DepartureTime = flights.DepartureTime;
        ArrivalTime = flights.ArrivalTime;
        SeatCount = flights.SeatCount;
        Status = flights.Status;
        Tickets = flights.Tickets.Select(t => new TicketsDto(t)).ToList();
    }


    public int Id { get; set; }

    public string From { get; set; }

    public string Destination { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public int SeatCount { get; set; }

    public FlightStatus Status { get; set; } = FlightStatus.Scheduled;



    public List<TicketsDto> Tickets { get; set; } = new();
}