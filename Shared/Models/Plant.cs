namespace Shared.Models;

public class Plant
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = "";
    public int CareLevel { get; set; }
    public bool IsPetFriendly { get; set; }

    public LightRequirement Light { get; set; }
    public WateringRequirement Watering { get; set; }
    public PlantCategory Category { get; set; }
}

public enum LightRequirement
{
    Low, Medium, High
}

public enum WateringRequirement
{
    Rare, Moderate, Frequent
}

public enum PlantCategory
{
    Foliage, Blooming, Succulents, LargeSize
}