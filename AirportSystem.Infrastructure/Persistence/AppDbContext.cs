using AirportSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Flights> Flights { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<Users> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUsers(modelBuilder);
        ConfigureFlights(modelBuilder);
        ConfigureTickets(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(128);

            entity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(128);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(u => u.PersonalId)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(u => u.Role)
                .IsRequired()
                .HasDefaultValue(UserRole.User);

            entity.HasMany(u => u.Tickets)
                  .WithOne(t => t.Passenger)
                  .HasForeignKey(t => t.PassengerId)
                  .OnDelete(DeleteBehavior.Restrict);


            var passwordHasher = new PasswordHasher<Users>();
            var adminUser = new Users
            {
                Id = 2,
                FirstName = "George",
                LastName = "Doe",
                Email = "georgedoe123@gmail.com",
                Role = UserRole.Owner,
                PhoneNumber = "555555555",
                PersonalId = "00000000000",
                Age = 30
            };
            adminUser.Password = passwordHasher.HashPassword(adminUser, "georgedoe123!");

            entity.HasData(adminUser);
        });
    }


    private void ConfigureFlights(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flights>(entity =>
        {
            entity.Property(f => f.From)
                .IsRequired()
                .HasMaxLength(128);

            entity.Property(f => f.Destination)
                .IsRequired()
                .HasMaxLength(128);

            entity.Property(f => f.SeatCount)
                .IsRequired();

            entity.Property(f => f.Status)
                .IsRequired()
                .HasDefaultValue(FlightStatus.Scheduled);

            entity.HasMany(f => f.Tickets)
                  .WithOne(t => t.Flight)
                  .HasForeignKey(t => t.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }


    private void ConfigureTickets(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tickets>(entity =>
        {
            entity.Property(t => t.SeatNumber)
                .IsRequired();

            entity.Property(t => t.Price)
                .HasColumnType("decimal(18,2)");

            entity.Property(t => t.IsCanceled)
                .HasDefaultValue(false);

            entity.HasIndex(t => new { t.FlightId, t.SeatNumber })
                  .IsUnique();
        });
    }
}
