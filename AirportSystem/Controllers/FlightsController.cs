using AirportSystem.Application.Dtos;
using AirportSystem.Application.Interfaces;
using AirportSystem.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportSystem.API.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightsService _flightsService;

    public FlightsController(IFlightsService flightsService)
    {
        _flightsService = flightsService;
    }

    // POST: api/flights
    [Authorize(Roles = "Admin, Owner")]
    [HttpPost]
    public async Task<ActionResult<FlightsDto>> Create(
        [FromBody] CreateOrUpdateFlightsDto dto)
    {
        var result = await _flightsService.CreateAsync(dto);
        return Ok(result);
    }

    // GET: api/flights
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightsDto>>> GetAll()
    {
        var result = await _flightsService.GetAllAsync();
        return Ok(result);
    }

    // GET: api/flights/count/5
    [AllowAnonymous]
    [HttpGet("count/{count}")]
    public async Task<ActionResult<IEnumerable<FlightsDto>>> GetByCount(int count)
    {
        var result = await _flightsService.GetByCountAsync(count);
        return Ok(result);
    }

    // GET: api/flights/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FlightsDto>> GetById(int id)
    {
        var result = await _flightsService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET: api/flights/search?from=Tbilisi&to=Paris
    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<FlightsDto>>> GetByRoute(
        [FromQuery] string? from,
        [FromQuery] string? to)
    {
        var result = await _flightsService.GetByRouteAsync(from, to);
        return Ok(result);
    }

    // GET: api/flights/with-tickets
    [HttpGet("with-tickets")]
    public async Task<ActionResult<IEnumerable<FlightsDto>>> GetFlightsWithTickets()
    {
        var result = await _flightsService.GetFlightsWithTicketsAsync();
        return Ok(result);
    }

    // PUT: api/flights/5
    [Authorize(Roles = "Admin, Owner")]
    [HttpPut("{id}")]
    public async Task<ActionResult<FlightsDto>> Update(
        int id,
        [FromBody] CreateOrUpdateFlightsDto dto)
    {
        var result = await _flightsService.UpdateAsync(id, dto);
        return Ok(result);
    }

    // PATCH: api/flights/5/status
    [Authorize(Roles = "Admin, Owner")]
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<FlightsDto>> UpdateStatus(
        int id,
        [FromBody] FlightStatus status)
    {
        var result = await _flightsService.UpdateStatusAsync(id, status);
        return Ok(result);
    }

    // DELETE: api/flights/5
    [Authorize(Roles = "Admin, Owner")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _flightsService.DeleteAsync(id);
        return NoContent();
    }
}
