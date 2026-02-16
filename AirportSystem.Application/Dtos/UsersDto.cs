using AirportSystem.Core.Entities;

namespace AirportSystem.Application.Dtos;

public class UsersDto
{
    public UsersDto (Users users)
    {
        Id = users.Id;
        FirstName = users.FirstName;
        LastName = users.LastName;
        Email = users.Email;
        Age = users.Age;
        Gender = users.Gender;
        PhoneNumber = users.PhoneNumber;
        PersonalId = users.PersonalId;
        Role = users.Role;
        Tickets = users.Tickets.Select(t => new TicketsDto(t)).ToList();
    } 



    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int? Age { get; set; } = null;

    public string? Gender { get; set; } = null;

    public string? PhoneNumber { get; set; } = null;

    public string PersonalId { get; set; }

    public UserRole Role { get; set; } = UserRole.User;




    public List<TicketsDto> Tickets { get; set; } = new();
}