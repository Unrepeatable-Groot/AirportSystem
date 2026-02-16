using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Interfaces;

public interface ITicketsRepository
{
    Task<IEnumerable<Tickets>> GetAllAsync();

    Task<IEnumerable<Tickets>> GetByCountAsync(int count);

    Task<Tickets?> GetByIdAsync(int id);

    Task<Tickets?> GetByIdWithInfoAsync(int id);

    Task<Tickets> CreateAsync(Tickets ticket);

    Task UpdateTicketAsync(Tickets ticket);

    Task DeleteTicketAsync(Tickets ticket);

    Task<IEnumerable<Tickets>> GetByPassengerIdAsync(int passengerId);

    Task<IEnumerable<Tickets>> GetByFlightIdAsync(int flightId);

    Task<bool> SeatTakenAsync(int flightId, int seatNumber);
}
