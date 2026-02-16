using AirportSystem.Application.Dtos;
using AirportSystem.Application.Interfaces;
using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Service;

public class FlightsService : IFlightsService
{
    private readonly IFlightsRepository _flightsRepository;

    public FlightsService(IFlightsRepository flightsRepository)
    {
        _flightsRepository = flightsRepository;
    }

    public async Task<FlightsDto> CreateAsync(CreateOrUpdateFlightsDto dto)
    {
        var flight = new Flights
        {
            From = dto.From,
            Destination = dto.Destination,
            DepartureTime = dto.DepartureTime,
            ArrivalTime = dto.ArrivalTime,
            SeatCount = dto.SeatCount,
            Status = dto.Status
        };

        var created = await _flightsRepository.CreateAsync(flight);
        return new FlightsDto(created);
    }

    public async Task<IEnumerable<FlightsDto>> GetAllAsync()
    {
        var flights = await _flightsRepository.GetAllAsync();
        return flights.Select(f => new FlightsDto(f));
    }

    public async Task<IEnumerable<FlightsDto>> GetByCountAsync(int count)
    {
        var flights = await _flightsRepository.GetByCountAsync(count);
        return flights.Select(f => new FlightsDto(f));
    }

    public async Task<FlightsDto?> GetByIdAsync(int id)
    {
        var flight = await _flightsRepository.GetByIdAsync(id);
        return flight == null ? null : new FlightsDto(flight);
    }

    public async Task<IEnumerable<FlightsDto>> GetByRouteAsync(string? from, string? to)
    {
        var flights = await _flightsRepository.GetByRouteAsync(from, to);
        return flights.Select(f => new FlightsDto(f));
    }

    public async Task<IEnumerable<FlightsDto>> GetFlightsWithTicketsAsync()
    {
        var flights = await _flightsRepository.GetAllWithTicketsAsync();

        return flights
            .Where(f => f.Tickets != null && f.Tickets.Count > 0)
            .Select(f => new FlightsDto(f));
    }

    public async Task<FlightsDto> UpdateAsync(int id, CreateOrUpdateFlightsDto dto)
    {
        var flight = await _flightsRepository.GetByIdAsync(id)
            ?? throw new Exception($"Flight with ID {id} does not exist.");

        flight.From = dto.From;
        flight.Destination = dto.Destination;
        flight.DepartureTime = dto.DepartureTime;
        flight.ArrivalTime = dto.ArrivalTime;
        flight.SeatCount = dto.SeatCount;
        flight.Status = dto.Status;

        await _flightsRepository.UpdateFlightAsync(flight);

        return new FlightsDto(flight);
    }

    public async Task<FlightsDto> UpdateStatusAsync(int id, FlightStatus status)
    {
        var flight = await _flightsRepository.GetByIdAsync(id)
            ?? throw new Exception($"Flight with ID {id} does not exist.");

        flight.Status = status;
        await _flightsRepository.UpdateFlightAsync(flight);

        return new FlightsDto(flight);
    }

    public async Task DeleteAsync(int id)
    {
        var flight = await _flightsRepository.GetByIdAsync(id)
            ?? throw new Exception($"Flight with ID {id} does not exist.");

        await _flightsRepository.DeleteFlightAsync(flight);
    }
}
