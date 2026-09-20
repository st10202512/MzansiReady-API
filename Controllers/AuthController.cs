using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MzansiReady.Api.Data;
using MzansiReady.Api.Dtos;
using MzansiReady.Api.Models;
using MzansiReady.Api.Services;

namespace MzansiReady.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly TokenService _tokens;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AppDbContext db, TokenService tokens, ILogger<AuthController> logger)
    {
        _db = db;
        _tokens = tokens;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (await _db.Users.AnyAsync(u => u.Email == email))
        {
            return Conflict(new { message = "An account with this email already exists." });
        }

        var user = new User
        {
            FullName = request.FullName.Trim(),
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        _logger.LogInformation("New user registered: {UserId}", user.UserId);

        return Ok(BuildResponse(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        return Ok(BuildResponse(user));
    }

    private AuthResponse BuildResponse(User user) => new(
        _tokens.CreateToken(user),
        user.UserId,
        user.FullName,
        user.Email,
        user.PreferredLanguage,
        user.Theme,
        user.NotificationsEnabled);
}