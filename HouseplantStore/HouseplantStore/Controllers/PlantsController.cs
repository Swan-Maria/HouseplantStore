using HouseplantStore.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Shared.Models;

namespace HouseplantStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PlantsController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<List<Plant>>> Get()
    {
        return await _context.Plants.ToListAsync();
    }
}