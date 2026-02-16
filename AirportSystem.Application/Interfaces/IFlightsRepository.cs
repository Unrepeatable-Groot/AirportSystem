using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Interfaces;

public interface IFlightsRepository
{
    Task<IEnumerable<Flights>> GetAllAsync();

    Task<IEnumerable<Flights>> GetByCountAsync(int count);

    Task<IEnumerable<Flights>> GetByRouteAsync(string? from, string? destination);

    Task<IEnumerable<Flights>> GetAllWithTicketsAsync();

    Task<Flights?> GetByIdAsync(int id);

    Task<Flights?> GetByIdWithTicketsAsync(int id);

    Task<Flights> CreateAsync(Flights flight);

    Task UpdateFlightAsync(Flights flight);

    Task DeleteFlightAsync(Flights flight);

    Task<bool> HasAvailableSeatsAsync(int flightId);

    Task<int> CountTicketsAsync(int flightId);
}

