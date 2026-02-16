using AirportSystem.Application.Interfaces;
using AirportSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Infrastructure.Persistence;

public class TicketsRepository : ITicketsRepository
{
    private readonly AppDbContext _context;

    public TicketsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Tickets> CreateAsync(Tickets ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<IEnumerable<Tickets>> GetAllAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<IEnumerable<Tickets>> GetByCountAsync(int count)
    {
        return await _context.Tickets
            .Take(count)
            .ToListAsync();
    }

    public async Task<Tickets?> GetByIdAsync(int id)
    {
        return await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tickets?> GetByIdWithInfoAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tickets>> GetByFlightIdAsync(int flightId)
    {
        return await _context.Tickets
            .Include(t => t.Passenger)
            .Where(t => t.FlightId == flightId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tickets>> GetByPassengerIdAsync(int passengerId)
    {
        return await _context.Tickets
            .Include(t => t.Flight)
            .Where(t => t.PassengerId == passengerId)
            .ToListAsync();
    }

    public async Task<bool> SeatTakenAsync(int flightId, int seatNumber)
    {
        return await _context.Tickets
            .AnyAsync(t => t.FlightId == flightId && t.SeatNumber == seatNumber);
    }

    public async Task UpdateTicketAsync(Tickets ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(Tickets ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }
}
