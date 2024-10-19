namespace AutoManagerAPI.Models;

public class CarService
{
    //C# - Contrutor da classe
    public CarService()
    {
        Id = Guid.NewGuid().ToString();
    }
    //C# - Atributo com get e set
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    
}