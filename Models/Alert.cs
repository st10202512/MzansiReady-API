using System.ComponentModel.DataAnnotations;

namespace MzansiReady.Api.Models;

public class Alert
{
    [Key]
    public int AlertId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public string Area { get; set; } = "National";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}