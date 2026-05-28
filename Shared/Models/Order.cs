namespace Shared.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public double TotalPrice { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}