using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Interfaces;

public interface IFlightsRepository
{
    Task<List<Flights>> GetAllAsync();

    Task<List<Flights>> GetByCountAsync(int count);

    Task<List<Flights>> GetByRouteAsync(string? from, string? destination);

    Task<List<Flights>> GetAllWithTicketsAsync();

    Task<Flights?> GetByIdAsync(int id);

    Task<Flights?> GetByIdWithTicketsAsync(int id);

    Task<Flights> CreateAsync(Flights flight);

    Task UpdateFlightAsync(Flights flight);

    Task DeleteFlightAsync(Flights flight);

    Task<bool> HasAvailableSeatsAsync(int flightId);

    Task<int> CountTicketsAsync(int flightId);
}

