namespace AutoManagerAPI.Models;
using AutoManagerAPI.dto;

public class Client 
{
    public Client() 
    {
        Id = Guid.NewGuid().ToString();
        Orders = [];
        Cars = [];
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set;}
    public List<Order> Orders { get; set; }
    public List<Car> Cars { get; set; }
}