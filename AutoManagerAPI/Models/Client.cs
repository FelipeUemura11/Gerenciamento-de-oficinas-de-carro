namespace AutoManagerAPI.Models;

public class Client 
{
    public Client() 
    {
        Id = Guid.NewGuid().ToString();
        Orders = new List<Order>();
        Cars = new List<Car>();
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set;}
    public List<Order> Orders { get; set; }
    public List<Car> Cars { get; set; }
}