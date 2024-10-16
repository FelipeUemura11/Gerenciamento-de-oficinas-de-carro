namespace AutoManagerAPI.Models;

public class Car
{
    //C# - Contrutor da classe
    public Car()
    {
        Id = Guid.NewGuid().ToString();
    }
    //C# - Atributo com get e set
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Plate { get; set; }
    public string? Color { get; set; }
    public Client Client { get; set; }
    
}