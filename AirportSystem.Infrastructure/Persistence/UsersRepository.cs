using AirportSystem.Application.Interfaces;
using AirportSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Infrastructure.Persistence;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Users> CreateAsync(Users user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<IEnumerable<Users>> GetByCountAsync(int count)
    {
        return await _context.Users
            .Take(count)
            .ToListAsync();
    }

    public async Task<Users?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Users>> GetAllWithTicketsAsync()
    {
        return await _context.Users
            .Include(u => u.Tickets)
            .ThenInclude(t => t.Flight)
            .ToListAsync();
    }

    public async Task<Users?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Users?> GetByIdWithTicketsAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Tickets)
            .ThenInclude(t => t.Flight)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> PersonalIdExistsAsync(string personalId)
    {
        return await _context.Users
            .AnyAsync(u => u.PersonalId == personalId);
    }

    public async Task UpdateUserAsync(Users user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Users user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
