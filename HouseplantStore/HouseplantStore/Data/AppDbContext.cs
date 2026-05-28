using Microsoft.EntityFrameworkCore;

using Shared.Models;

namespace HouseplantStore.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Plant> Plants { get; set; } = null!;
public DbSet<Order> Orders { get; set; } = null!;
public DbSet<OrderItem> OrderItems { get; set; } = null!;

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Plant>().Property(p => p.Price).HasColumnType("numeric(18,2)");
    modelBuilder.Entity<Order>().Property(o => o.TotalPrice).HasColumnType("numeric(18,2)");
    modelBuilder.Entity<OrderItem>().Property(oi => oi.PriceAtPurchase).HasColumnType("numeric(18,2)");
}
}