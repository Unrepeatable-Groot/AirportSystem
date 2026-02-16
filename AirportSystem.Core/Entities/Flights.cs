namespace AirportSystem.Core.Entities;

public class Flights
{
    public int Id { get; set; }

    public string From { get; set; }

    public string Destination { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public int SeatCount { get; set; }

    public FlightStatus Status { get; set; } = FlightStatus.Scheduled;




    public List<Tickets> Tickets { get; set; } = new List<Tickets>();
}

public enum FlightStatus
{
    Scheduled,
    InProgress,
    Completed
}
