using AirportSystem.Application.Dtos;
using AirportSystem.Application.Interfaces;
using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Service;

public class TicketsService : ITicketsService
{
    private readonly ITicketsRepository _ticketsRepository;

    public TicketsService(ITicketsRepository ticketsRepository)
    {
        _ticketsRepository = ticketsRepository;
    }

    public async Task<TicketsDto> CreateAsync(CreateOrUpdateTicketsDto dto)
    {
        var ticket = new Tickets
        {
            SeatNumber = dto.SeatNumber,
            FlightId = dto.FlightId,
            PassengerId = dto.PassengerId,
            Price = dto.Price,
            IsCanceled = dto.IsCanceled
        };

        var created = await _ticketsRepository.CreateAsync(ticket);
        return new TicketsDto(created);
    }

    public async Task<IEnumerable<TicketsDto>> GetAllAsync()
    {
        var tickets = await _ticketsRepository.GetAllAsync();
        return tickets.Select(t => new TicketsDto(t));
    }

    public async Task<IEnumerable<TicketsDto>> GetByCountAsync(int count)
    {
        var tickets = await _ticketsRepository.GetByCountAsync(count);
        return tickets.Select(t => new TicketsDto(t));
    }

    public async Task<TicketsDto?> GetByIdAsync(int id)
    {
        var ticket = await _ticketsRepository.GetByIdAsync(id);
        return ticket == null ? null : new TicketsDto(ticket);
    }

    public async Task<IEnumerable<TicketsDto>> GetByUserIdAsync(int userId)
    {
        var tickets = await _ticketsRepository.GetByPassengerIdAsync(userId);
        return tickets.Select(t => new TicketsDto(t));
    }

    public async Task<IEnumerable<TicketsDto>> GetByFlightIdAsync(int flightId)
    {
        var tickets = await _ticketsRepository.GetByFlightIdAsync(flightId);
        return tickets.Select(t => new TicketsDto(t));
    }

    public async Task<TicketsDto> CancelTicketAsync(int id)
    {
        var entity = await _ticketsRepository.GetByIdAsync(id)
            ?? throw new Exception($"Ticket with ID {id} does not exist.");

        if (entity.IsCanceled)
            throw new Exception("Ticket is already canceled.");

        entity.IsCanceled = true;

        await _ticketsRepository.UpdateTicketAsync(entity);
        return new TicketsDto(entity);
    }

    public async Task<TicketsDto> UpdateAsync(int id, CreateOrUpdateTicketsDto dto)
    {
        var entity = await _ticketsRepository.GetByIdAsync(id)
            ?? throw new Exception($"Ticket with ID {id} does not exist.");

        entity.FlightId = dto.FlightId;
        entity.PassengerId = dto.PassengerId;
        entity.SeatNumber = dto.SeatNumber;
        entity.Price = dto.Price;

        await _ticketsRepository.UpdateTicketAsync(entity);
        return new TicketsDto(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _ticketsRepository.GetByIdAsync(id)
            ?? throw new Exception($"Ticket with ID {id} does not exist.");

        await _ticketsRepository.DeleteTicketAsync(entity);
    }
}
