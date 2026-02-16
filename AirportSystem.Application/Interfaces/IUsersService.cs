using AirportSystem.Application.Dtos;

namespace AirportSystem.Application.Interfaces;

public interface IUsersService
{
    Task<IEnumerable<UsersDto>> GetAllAsync();

    Task<IEnumerable<UsersDto>> GetByCountAsync(int count);

    Task<UsersDto?> GetByIdAsync(int id);

    Task<UsersDto> CreateAsync(UsersCreateDto dto);

    Task DeleteAsync(int id);

    Task<UsersDto> UpdateAsync(int id, UsersUpdateDto dto);

    Task<UsersDto> UpdateRoleAsync(int id, UsersRoleUpdateDto dto);

    Task<UsersDto> ChangeEmailAsync(int id, ChangeEmailDto dto);

    Task<UsersDto> ChangePasswordAsync(int id, ChangePasswordDto dto);

    Task<UsersDto?> GetByIdWithTicketsAsync(int id);

    Task<IEnumerable<UsersDto>> GetUsersWithTicketsAsync();

    Task<string> AuthenticateAsync(UserLoginDto dto);
}
