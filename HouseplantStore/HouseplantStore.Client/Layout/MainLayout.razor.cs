using System.Net.Http.Json;

using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Services;

using Shared.Models;

namespace HouseplantStore.Client.Layout;

public partial class MainLayout : LayoutComponentBase, IBrowserViewportObserver, IAsyncDisposable, IDisposable
{
    [Inject] private HttpClient Http { get; set; } = null!;

    private List<Plant> _plants = new();
    private List<Plant> _filteredPlants = new();

    private bool _cartDrawer;
    private bool _isDarkMode;
    private bool _isLargeScreen = true;

    public Guid Id { get; } = Guid.NewGuid();
    public ResizeOptions ResizeOptions { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        CartService.OnChange += StateHasChanged;

        _plants = await ProductService.GetPlantsAsync();
        _filteredPlants = _plants;

        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await ViewportService.SubscribeAsync(this, fireImmediately: true);
    }

    public async Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs args)
    {
        _isLargeScreen = args.Breakpoint >= Breakpoint.Lg;
        await InvokeAsync(StateHasChanged);
    }

    private void NavigateTo(string path) =>
        NavigationManager.NavigateTo(path);

    private void ToggleTheme() =>
        _isDarkMode = !_isDarkMode;

    private void DrawerToggle() =>
        _cartDrawer = !_cartDrawer;

    private async Task Checkout()
    {
        var order = new
        {
            Items = CartService.Items.Select(i => new { PlantId = i.Plant.Id, Quantity = i.Quantity }),
            TotalPrice = CartService.Total
        };

        var response = await Http.PostAsJsonAsync("api/orders", order);

        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add("Order successfully placed!", Severity.Success);
            CartService.ClearCart();
            _cartDrawer = false;
        }
        else
        {
            Snackbar.Add("Error placing order.", Severity.Error);
        }
    }

    public void Dispose()
        => CartService.OnChange -= StateHasChanged;

    public async ValueTask DisposeAsync()
        => await ViewportService.UnsubscribeAsync(this);
}