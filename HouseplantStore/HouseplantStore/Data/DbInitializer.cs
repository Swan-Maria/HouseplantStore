using System.Text.Json;

using Shared.Models;

namespace HouseplantStore.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Plants.Any()) return;

        var filePath = "wwwroot/products.json";
        if (!File.Exists(filePath)) return;

        var jsonData = await File.ReadAllTextAsync(filePath);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var plants = JsonSerializer.Deserialize<List<Plant>>(jsonData, options);

        if (plants != null)
        {
            await context.Plants.AddRangeAsync(plants);
            await context.SaveChangesAsync();
        }
    }
}