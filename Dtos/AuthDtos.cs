using System.ComponentModel.DataAnnotations;

namespace MzansiReady.Api.Dtos;

public record RegisterRequest(
    [Required, StringLength(80, MinimumLength = 2)] string FullName,
    [Required, EmailAddress] string Email,
    [Required, StringLength(64, MinimumLength = 8)] string Password);

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password);

public record AuthResponse(
    string Token,
    int UserId,
    string FullName,
    string Email,
    string PreferredLanguage,
    string Theme,
    bool NotificationsEnabled);

public record SettingsDto(
    string PreferredLanguage,
    string Theme,
    bool NotificationsEnabled);

public record LocationDto(int LocationId, string Name, double Latitude, double Longitude);

public record CreateLocationRequest(
    [Required, StringLength(60, MinimumLength = 2)] string Name,
    [Range(-90, 90)] double Latitude,
    [Range(-180, 180)] double Longitude);