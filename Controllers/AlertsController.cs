using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MzansiReady.Api.Data;

namespace MzansiReady.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/alerts")]
public class AlertsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AlertsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var alerts = await _db.Alerts
            .OrderByDescending(a => a.CreatedAt)
            .Take(20)
            .ToListAsync();

        return Ok(alerts);
    }
}