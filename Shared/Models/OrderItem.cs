namespace Shared.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int PlantId { get; set; }
    public int Quantity { get; set; }
    public double PriceAtPurchase { get; set; }
}