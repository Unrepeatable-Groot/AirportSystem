using AirportSystem.Application.Interfaces;
using AirportSystem.Application.Service;
using AirportSystem.Core.Entities;
using AirportSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = Environments.Production
});

builder.Configuration.AddJsonFile(
    "appsettings.json",
    optional: false,
    reloadOnChange: false
);


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "5000"));
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IFlightsRepository, FlightsRepository>();
builder.Services.AddScoped<ITicketsRepository, TicketsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();


builder.Services.AddScoped<IFlightsService, FlightsService>();
builder.Services.AddScoped<ITicketsService, TicketsService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();

var jwtKey = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AirportSystem API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        Console.WriteLine("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database migration failed: " + ex.Message);
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirportSystem API V1");
    });
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
