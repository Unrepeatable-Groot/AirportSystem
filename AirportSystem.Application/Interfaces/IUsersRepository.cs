using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<Users>> GetAllAsync();

    Task<IEnumerable<Users>> GetByCountAsync(int count);

    Task<Users?> GetByIdAsync(int id);

    Task<Users> CreateAsync(Users user);

    Task UpdateUserAsync(Users user);

    Task DeleteUserAsync(Users user);

    Task<Users?> GetByEmailAsync(string email);

    Task<bool> PersonalIdExistsAsync(string personalId);

    Task<Users?> GetByIdWithTicketsAsync(int id);

    Task<IEnumerable<Users>> GetAllWithTicketsAsync();
}