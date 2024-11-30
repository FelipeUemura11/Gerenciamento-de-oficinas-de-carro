namespace AutoManagerAPI.Models;

public class Car
{
    public Car()
    {
        Id = Guid.NewGuid().ToString();
        OrdersHistoric = new List<Order>();
    }
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