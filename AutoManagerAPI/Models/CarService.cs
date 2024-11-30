namespace AutoManagerAPI.Models;

public class CarService
{
    public CarService()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    
}