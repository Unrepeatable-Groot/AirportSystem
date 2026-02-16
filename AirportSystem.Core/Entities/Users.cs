namespace AirportSystem.Core.Entities;

public class Users
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int? Age { get; set; } = null;

    public string? Gender { get; set; } = null;

    public string? PhoneNumber { get; set; } = null;

    public string PersonalId { get; set; }

    public UserRole Role { get; set; } = UserRole.User;




    public List<Tickets> Tickets { get; set; } = new();
}

public enum UserRole
{
    User,
    VIP,
    Admin,
    Owner
}