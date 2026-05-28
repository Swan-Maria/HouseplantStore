using System.Net.Http.Json;

using Shared.Models;

namespace HouseplantStore.Client.Services;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Plant>> GetPlantsAsync()
    {
        return await _http.GetFromJsonAsync<List<Plant>>("api/plants") ?? new();
    }
}