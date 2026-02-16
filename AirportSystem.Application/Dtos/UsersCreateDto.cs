namespace AirportSystem.Application.Dtos;

public class UsersCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PersonalId { get; set; } = null!;
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
}
