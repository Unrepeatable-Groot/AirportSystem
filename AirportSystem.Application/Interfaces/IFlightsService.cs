using AirportSystem.Application.Dtos;
using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Interfaces;

public interface IFlightsService
{
    Task<IEnumerable<FlightsDto>> GetAllAsync();

    Task<IEnumerable<FlightsDto>> GetByCountAsync(int count);

    Task<IEnumerable<FlightsDto>> GetByRouteAsync(string? from, string? to);

    Task<FlightsDto?> GetByIdAsync(int id);

    Task<FlightsDto> CreateAsync(CreateOrUpdateFlightsDto dto);

    Task DeleteAsync(int id);

    Task<FlightsDto> UpdateAsync(int id, CreateOrUpdateFlightsDto dto);

    Task<FlightsDto> UpdateStatusAsync(int id, FlightStatus status);

    Task<IEnumerable<FlightsDto>> GetFlightsWithTicketsAsync();
}
