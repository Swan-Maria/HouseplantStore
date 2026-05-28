using HouseplantStore.Data;

using Microsoft.AspNetCore.Mvc;

using Shared.Models;

namespace HouseplantStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Ok();
    }
}