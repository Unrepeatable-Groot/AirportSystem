using AirportSystem.Application.Dtos;
using AirportSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketsService _ticketsService;

    public TicketsController(ITicketsService ticketsService)
    {
        _ticketsService = ticketsService;
    }


    [HttpPost]
    public async Task<ActionResult<TicketsDto>> Create(
        [FromBody] CreateOrUpdateTicketsDto dto)
    {
        var result = await _ticketsService.CreateAsync(dto);
        return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketsDto>>> GetAll()
    {
        var result = await _ticketsService.GetAllAsync();
        return Ok(result);
    }


    [HttpGet("count/{count}")]
    public async Task<ActionResult<IEnumerable<TicketsDto>>> GetByCount(int count)
    {
        var result = await _ticketsService.GetByCountAsync(count);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TicketsDto>> GetById(int id)
    {
        var result = await _ticketsService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TicketsDto>>> GetByUser(int userId)
    {
        var result = await _ticketsService.GetByUserIdAsync(userId);
        return Ok(result);
    }


    [HttpGet("flight/{flightId}")]
    public async Task<ActionResult<IEnumerable<TicketsDto>>> GetByFlight(int flightId)
    {
        var result = await _ticketsService.GetByFlightIdAsync(flightId);
        return Ok(result);
    }


    [HttpPatch("{id}/cancel")]
    public async Task<ActionResult<TicketsDto>> Cancel(int id)
    {
        var result = await _ticketsService.CancelTicketAsync(id);
        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<TicketsDto>> Update(
        int id,
        [FromBody] CreateOrUpdateTicketsDto dto)
    {
        var result = await _ticketsService.UpdateAsync(id, dto);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ticketsService.DeleteAsync(id);
        return NoContent();
    }
}
