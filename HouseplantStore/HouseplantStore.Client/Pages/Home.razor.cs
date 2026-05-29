using Shared.Models;

namespace HouseplantStore.Client.Pages;

public partial class Home
{
    private List<Plant>? _plants;
    private List<Plant>? _filteredPlants;
    private Dictionary<int, int> _cartCounts = new();
    
    private PlantCategory? _selectedCategory;
    private int? _selectedCareLevel;
    private LightRequirement? _selectedLight;
    private WateringRequirement? _selectedWatering;
    private bool? _isPetFriendly;
    private decimal _maxPrice = 100;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            _plants = await ProductService.GetPlantsAsync();
            _filteredPlants = _plants.ToList();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading plants: {ex.Message}");
            _plants = new List<Plant>();
            _filteredPlants = new List<Plant>();
        }
    }
    
    private void FilterByCategory(PlantCategory? category)
    {
        _selectedCategory = category;
        ApplyFilters();
    }

    private void FilterByCareLevel(bool value, int level)
    {
        _selectedCareLevel = value ? level : null;
        ApplyFilters();
    }

    private void OnPriceChanged(decimal value)
    {
        _maxPrice = value;
        ApplyFilters();
    }

    private void OnPetFriendlyChanged(bool? val) 
    {
        _isPetFriendly = (val == true) ? true : null;
        ApplyFilters();
    }

    private void AddToCart(Plant plant)
    {
        CartService.AddToCart(plant);
        _cartCounts[plant.Id] = _cartCounts.GetValueOrDefault(plant.Id, 0) + 1;
    }

    public int GetPlantCount(int plantId) => _cartCounts.GetValueOrDefault(plantId, 0);
    
    private void ApplyFilters()
    {
        if (_plants == null) return;

        IEnumerable<Plant> query = _plants;

        if (_selectedCategory.HasValue) query = query.Where(p => p.Category == _selectedCategory);
        if (_selectedCareLevel.HasValue) query = query.Where(p => p.CareLevel == _selectedCareLevel);
        if (_selectedLight.HasValue) query = query.Where(p => p.Light == _selectedLight);
        if (_selectedWatering.HasValue) query = query.Where(p => p.Watering == _selectedWatering);
        if (_isPetFriendly.HasValue) query = query.Where(p => p.IsPetFriendly == _isPetFriendly);
        
        query = query.Where(p => p.Price <= _maxPrice);

        _filteredPlants = query.ToList();
    }
}
