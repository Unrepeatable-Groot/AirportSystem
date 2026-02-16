using AirportSystem.Application.Dtos;

namespace AirportSystem.Application.Interfaces;

public interface ITicketsService
{
    Task<IEnumerable<TicketsDto>> GetAllAsync();

    Task<IEnumerable<TicketsDto>> GetByCountAsync(int count);

    Task<TicketsDto?> GetByIdAsync(int id);

    Task<TicketsDto> CreateAsync(CreateOrUpdateTicketsDto dto);

    Task DeleteAsync(int id);

    Task<TicketsDto> UpdateAsync(int id, CreateOrUpdateTicketsDto dto);

    Task<IEnumerable<TicketsDto>> GetByUserIdAsync(int userId);

    Task<IEnumerable<TicketsDto>> GetByFlightIdAsync(int flightId);

    Task<TicketsDto> CancelTicketAsync(int id);
}
