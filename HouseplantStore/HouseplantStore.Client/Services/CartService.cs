using Shared.Models;

namespace HouseplantStore.Client.Services;

public class CartItem
{
    public Plant Plant { get; set; } = null!;
    public int Quantity { get; set; }
}

public class CartService
{
    public List<CartItem> Items { get; private set; } = new();

    public event Action? OnChange;

    public int TotalItems => Items.Sum(i => i.Quantity);
    public decimal Subtotal => Items.Sum(i => i.Plant.Price * i.Quantity);
    public decimal Shipping => 5.00m;
    public decimal Tax => Subtotal * 0.08m;
    public decimal Total => Subtotal + Shipping + Tax;

    public void AddToCart(Plant plant)
    {
        var item = Items.FirstOrDefault(i => i.Plant.Id == plant.Id);
        if (item == null) Items.Add(new CartItem { Plant = plant, Quantity = 1 });
        else item.Quantity++;

        OnChange?.Invoke();
    }

    public void RemoveOne(Plant plant)
    {
        var item = Items.FirstOrDefault(i => i.Plant.Id == plant.Id);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0) Items.Remove(item);
            OnChange?.Invoke();
        }
    }

    public void ClearCart()
    {
        Items.Clear();
        OnChange?.Invoke();
    }
}