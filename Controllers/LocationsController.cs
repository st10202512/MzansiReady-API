using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MzansiReady.Api.Data;
using MzansiReady.Api.Dtos;
using MzansiReady.Api.Models;

namespace MzansiReady.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/locations")]
public class LocationsController : ControllerBase
{
    private readonly AppDbContext _db;

    public LocationsController(AppDbContext db) => _db = db;

    private int CurrentUserId => int.Parse(User.FindFirstValue("userId")!);

    [HttpGet]
    public async Task<ActionResult<List<LocationDto>>> GetAll()
    {
        var locations = await _db.SavedLocations
            .Where(l => l.UserId == CurrentUserId)
            .OrderBy(l => l.CreatedAt)
            .Select(l => new LocationDto(l.LocationId, l.Name, l.Latitude, l.Longitude))
            .ToListAsync();

        return Ok(locations);
    }

    [HttpPost]
    public async Task<ActionResult<LocationDto>> Create(CreateLocationRequest request)
    {
        var location = new SavedLocation
        {
            UserId = CurrentUserId,
            Name = request.Name.Trim(),
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            CreatedAt = DateTime.UtcNow
        };

        _db.SavedLocations.Add(location);
        await _db.SaveChangesAsync();

        return Ok(new LocationDto(location.LocationId, location.Name, location.Latitude, location.Longitude));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var location = await _db.SavedLocations
            .FirstOrDefaultAsync(l => l.LocationId == id && l.UserId == CurrentUserId);

        if (location is null) return NotFound();

        _db.SavedLocations.Remove(location);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}