namespace AirportSystem.Application.Dtos;

public class CreateOrUpdateTicketsDto
{
    public int SeatNumber { get; set; }

    public int FlightId { get; set; }

    public int PassengerId { get; set; }

    public decimal Price { get; set; }

    public bool IsCanceled { get; set; }
}
