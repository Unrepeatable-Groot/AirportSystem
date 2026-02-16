namespace AirportSystem.Core.Entities;

public class Tickets
{
    public int Id { get; set; }

    public int SeatNumber { get; set; }

    public int FlightId { get; set; }

    public int PassengerId { get; set; }

    public decimal Price { get; set; }

    public bool IsCanceled { get; set; }

    public Flights? Flight { get; set; } = null!;
    public Users? Passenger { get; set; } = null!;
}