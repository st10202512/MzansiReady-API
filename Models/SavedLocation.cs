using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MzansiReady.Api.Models;

public class SavedLocation
{
    [Key]
    public int LocationId { get; set; }

    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public User? User { get; set; }
}