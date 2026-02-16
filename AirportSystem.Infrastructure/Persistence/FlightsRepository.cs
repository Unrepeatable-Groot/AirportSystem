using AirportSystem.Application.Interfaces;
using AirportSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Infrastructure.Persistence;

public class FlightsRepository : IFlightsRepository
{
    private readonly AppDbContext _context;

    public FlightsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Flights> CreateAsync(Flights flight)
    {
        await _context.Flights.AddAsync(flight);
        await _context.SaveChangesAsync();
        return flight;
    }

    public async Task<IEnumerable<Flights>> GetAllAsync()
    {
        return await _context.Flights.ToListAsync();
    }

    public async Task<IEnumerable<Flights>> GetByCountAsync(int count)
    {
        return await _context.Flights
            .Take(count)
            .ToListAsync();
    }

    public async Task<Flights?> GetByIdAsync(int id)
    {
        return await _context.Flights
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CountTicketsAsync(int flightId)
    {
        return await _context.Tickets
            .CountAsync(t => t.FlightId == flightId);
    }

    public async Task<Flights?> GetByIdWithTicketsAsync(int id)
    {
        return await _context.Flights
            .Include(f => f.Tickets)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Flights>> GetByRouteAsync(string? from, string? destination)
    {
        var query = _context.Flights.AsQueryable();

        if (!string.IsNullOrEmpty(from))
            query = query.Where(f => f.From == from);

        if (!string.IsNullOrEmpty(destination))
            query = query.Where(f => f.Destination == destination);

        return await query.ToListAsync();
    }

    public async Task<bool> HasAvailableSeatsAsync(int flightId)
    {
        var flight = await _context.Flights.FindAsync(flightId);

        if (flight == null)
            return false;

        var soldTickets = await _context.Tickets
            .CountAsync(t => t.FlightId == flightId);

        return soldTickets < flight.SeatCount;
    }

    public async Task UpdateFlightAsync(Flights flight)
    {
        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFlightAsync(Flights flight)
    {
        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Flights>> GetAllWithTicketsAsync()
    {
        return await _context.Flights
            .Include(f => f.Tickets)
            .ToListAsync();
    }
}