namespace AutoManagerAPI.Models;

public class Car
{
    //C# - Contrutor da classe
    public Car()
    {
        Id = Guid.NewGuid().ToString();
        OrdersHistoric = [];
    }
    //C# - Atributo com get e set
    public string Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
    public string Year { get; set; }
    public string? Color { get; set; }
    public string ClientId { get; set; }
    public Client? Client { get; set; }
    public List<Order> OrdersHistoric { get; set; }
}