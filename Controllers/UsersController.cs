using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MzansiReady.Api.Data;
using MzansiReady.Api.Dtos;

namespace MzansiReady.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db) => _db = db;

    private int CurrentUserId => int.Parse(User.FindFirstValue("userId")!);

    [HttpGet("settings")]
    public async Task<ActionResult<SettingsDto>> GetSettings()
    {
        var user = await _db.Users.FindAsync(CurrentUserId);
        if (user is null) return NotFound();
        return Ok(new SettingsDto(user.PreferredLanguage, user.Theme, user.NotificationsEnabled));
    }

    [HttpPut("settings")]
    public async Task<ActionResult<SettingsDto>> UpdateSettings([FromBody] SettingsDto dto)
    {
        var user = await _db.Users.FindAsync(CurrentUserId);
        if (user is null) return NotFound();

        var allowedLanguages = new[] { "en", "zu" };
        var allowedThemes = new[] { "light", "dark" };

        if (!allowedLanguages.Contains(dto.PreferredLanguage))
            return BadRequest(new { message = "Unsupported language." });
        if (!allowedThemes.Contains(dto.Theme))
            return BadRequest(new { message = "Unsupported theme." });

        user.PreferredLanguage = dto.PreferredLanguage;
        user.Theme = dto.Theme;
        user.NotificationsEnabled = dto.NotificationsEnabled;
        await _db.SaveChangesAsync();

        return Ok(new SettingsDto(user.PreferredLanguage, user.Theme, user.NotificationsEnabled));
    }
}