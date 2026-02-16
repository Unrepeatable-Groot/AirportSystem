using AirportSystem.Application.Dtos;
using AirportSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }


    [HttpPost]
    public async Task<ActionResult<UsersDto>> Create(
        [FromBody] UsersCreateDto dto)
    {
        var result = await _usersService.CreateAsync(dto);
        return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsersDto>>> GetAll()
    {
        var result = await _usersService.GetAllAsync();
        return Ok(result);
    }


    [HttpGet("count/{count}")]
    public async Task<ActionResult<IEnumerable<UsersDto>>> GetByCount(int count)
    {
        var result = await _usersService.GetByCountAsync(count);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UsersDto>> GetById(int id)
    {
        var result = await _usersService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpGet("with-tickets")]
    public async Task<ActionResult<IEnumerable<UsersDto>>> GetAllWithTickets()
    {
        var result = await _usersService.GetUsersWithTicketsAsync();
        return Ok(result);
    }


    [HttpGet("{id}/tickets")]
    public async Task<ActionResult<UsersDto>> GetByIdWithTickets(int id)
    {
        var result = await _usersService.GetByIdWithTicketsAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<UsersDto>> Update(
        int id,
        [FromBody] UsersUpdateDto dto)
    {
        var result = await _usersService.UpdateAsync(id, dto);
        return Ok(result);
    }


    [HttpPatch("{id}/email")]
    public async Task<ActionResult<UsersDto>> ChangeEmail(
        int id,
        [FromBody] ChangeEmailDto dto)
    {
        var result = await _usersService.ChangeEmailAsync(id, dto);
        return Ok(result);
    }


    [HttpPatch("{id}/password")]
    public async Task<ActionResult<UsersDto>> ChangePassword(
        int id,
        [FromBody] ChangePasswordDto dto)
    {
        var result = await _usersService.ChangePasswordAsync(id, dto);
        return Ok(result);
    }


    [Authorize(Roles = "Admin, Owner")]
    [HttpPatch("{id}/role")]
    public async Task<ActionResult<UsersDto>> UpdateRole(
        int id,
        [FromBody] UsersRoleUpdateDto dto)
    {
        var result = await _usersService.UpdateRoleAsync(id, dto);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _usersService.DeleteAsync(id);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
    [FromBody] UserLoginDto dto)
    {
        var token = await _usersService.AuthenticateAsync(dto);
        return Ok(token);
    }
}
