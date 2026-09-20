using MzansiReady.Api.Models;

namespace MzansiReady.Api.Data;

public static class Seed
{
    public static void EnsureAlerts(AppDbContext db)
    {
        if (db.Alerts.Any()) return;

        db.Alerts.AddRange(
            new Alert
            {
                Title = "Planned water interruption",
                Description = "Water supply will be interrupted in Soweto between 09:00 and 14:00.",
                Category = "Water",
                Area = "Johannesburg"
            },
            new Alert
            {
                Title = "Stage 3 load shedding",
                Description = "Stage 3 is in effect from 16:00.",
                Category = "Electricity",
                Area = "National"
            },
            new Alert
            {
                Title = "Severe thunderstorm warning",
                Description = "Heavy rain and hail expected over Gauteng this afternoon.",
                Category = "Weather",
                Area = "Gauteng"
            });

        db.SaveChanges();
    }
}