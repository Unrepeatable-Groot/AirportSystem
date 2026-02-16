using AirportSystem.Application.Dtos;
using AirportSystem.Application.Interfaces;
using AirportSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirportSystem.Application.Service;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher<Users> _passwordHasher;
    private readonly IConfiguration _configuration;

    public UsersService(
        IUsersRepository usersRepository,
        IPasswordHasher<Users> passwordHasher,
        IConfiguration configuration)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<UsersDto> CreateAsync(UsersCreateDto dto)
    {
        var existingByEmail = await _usersRepository.GetByEmailAsync(dto.Email);
        if (existingByEmail != null)
            throw new Exception($"Email '{dto.Email}' is already registered.");

        if (!string.IsNullOrWhiteSpace(dto.PersonalId) &&
            await _usersRepository.PersonalIdExistsAsync(dto.PersonalId))
            throw new Exception($"PersonalId '{dto.PersonalId}' is already registered.");

        var user = new Users
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Age = dto.Age,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            PersonalId = dto.PersonalId
        };

        user.Password = _passwordHasher.HashPassword(user, dto.Password);

        var created = await _usersRepository.CreateAsync(user);
        return new UsersDto(created);
    }

    public async Task<IEnumerable<UsersDto>> GetAllAsync()
    {
        var users = await _usersRepository.GetAllAsync();
        return users.Select(u => new UsersDto(u));
    }

    public async Task<IEnumerable<UsersDto>> GetByCountAsync(int count)
    {
        var users = await _usersRepository.GetByCountAsync(count);
        return users.Select(u => new UsersDto(u));
    }

    public async Task<UsersDto?> GetByIdAsync(int id)
    {
        var user = await _usersRepository.GetByIdAsync(id);
        return user == null ? null : new UsersDto(user);
    }

    public async Task<UsersDto?> GetByIdWithTicketsAsync(int id)
    {
        var user = await _usersRepository.GetByIdWithTicketsAsync(id);
        return user == null ? null : new UsersDto(user);
    }

    public async Task<IEnumerable<UsersDto>> GetUsersWithTicketsAsync()
    {
        var users = await _usersRepository.GetAllWithTicketsAsync();
        return users.Select(u => new UsersDto(u));
    }

    public async Task<UsersDto> ChangeEmailAsync(int id, ChangeEmailDto dto)
    {
        var user = await _usersRepository.GetByIdAsync(id)
            ?? throw new Exception($"User with ID {id} does not exist.");

        var byEmail = await _usersRepository.GetByEmailAsync(dto.NewEmail);
        if (byEmail != null && byEmail.Id != id)
            throw new Exception($"Email '{dto.NewEmail}' is already in use.");

        user.Email = dto.NewEmail;
        await _usersRepository.UpdateUserAsync(user);

        return new UsersDto(user);
    }

    public async Task<UsersDto> ChangePasswordAsync(int id, ChangePasswordDto dto)
    {
        var user = await _usersRepository.GetByIdAsync(id)
            ?? throw new Exception($"User with ID {id} does not exist.");

        var verifyResult = _passwordHasher.VerifyHashedPassword(
            user, user.Password, dto.CurrentPassword);

        if (verifyResult == PasswordVerificationResult.Failed)
            throw new Exception("Old password is incorrect.");

        user.Password = _passwordHasher.HashPassword(user, dto.NewPassword);
        await _usersRepository.UpdateUserAsync(user);

        return new UsersDto(user);
    }

    public async Task<UsersDto> UpdateAsync(int id, UsersUpdateDto dto)
    {
        var user = await _usersRepository.GetByIdAsync(id)
            ?? throw new Exception($"User with ID {id} does not exist.");

        if (dto.FirstName != null) user.FirstName = dto.FirstName;
        if (dto.LastName != null) user.LastName = dto.LastName;
        if (dto.Age.HasValue) user.Age = dto.Age;
        if (dto.Gender != null) user.Gender = dto.Gender;
        if (dto.PhoneNumber != null) user.PhoneNumber = dto.PhoneNumber;

        await _usersRepository.UpdateUserAsync(user);
        return new UsersDto(user);
    }

    public async Task<UsersDto> UpdateRoleAsync(int id, UsersRoleUpdateDto dto)
    {
        var user = await _usersRepository.GetByIdAsync(id)
            ?? throw new Exception($"User with ID {id} does not exist.");

        user.Role = dto.Role;
        await _usersRepository.UpdateUserAsync(user);

        return new UsersDto(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _usersRepository.GetByIdAsync(id)
            ?? throw new Exception($"User with ID {id} does not exist.");

        await _usersRepository.DeleteUserAsync(user);
    }

    public async Task<string> AuthenticateAsync(UserLoginDto dto)
    {
        var user = await _usersRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            throw new Exception("Invalid credentials.");

        var result = _passwordHasher.VerifyHashedPassword(
            user, user.Password, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid credentials.");

        return GenerateToken(user);
    }

    // JWT გენერაცია რჩება sync — აქ DB ოპერაცია არ არის
    private string GenerateToken(Users user)
    {
        var key = _configuration["Jwt:Key"]
                  ?? throw new Exception("JWT Key is missing.");

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expiryMinutesString = _configuration["Jwt:ExpiryMinutes"];

        int expiryMinutes = 240;
        if (!string.IsNullOrEmpty(expiryMinutesString) &&
            int.TryParse(expiryMinutesString, out var tmp))
            expiryMinutes = tmp;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}