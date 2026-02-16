using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Dtos;

public class TicketsDto
{
    public TicketsDto(Tickets tickets) 
    { 
        Id = tickets.Id;
        SeatNumber = tickets.SeatNumber;
        FlightId = tickets.FlightId;
        PassengerId = tickets.PassengerId;
        Price = tickets.Price;
        IsCanceled = tickets.IsCanceled;
    }


    public int Id { get; set; }

    public int SeatNumber { get; set; }

    public int FlightId { get; set; }

    public int PassengerId { get; set; }

    public decimal Price { get; set; }

    public bool IsCanceled { get; set; }
}
