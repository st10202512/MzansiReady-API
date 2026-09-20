using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MzansiReady.Api.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public string PreferredLanguage { get; set; } = "en";
    public string Theme { get; set; } = "light";
    public bool NotificationsEnabled { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<SavedLocation> SavedLocations { get; set; } = new();
}