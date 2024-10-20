namespace AutoManagerAPI.Models;

public class Order {
    public Order() {
        Id = Guid.NewGuid().ToString();
        Status = false;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CarServiceId { get; set; }
    public string ClientId { get; set; }
    public string CarId { get; set; }
    public CarService? CarService { get; set; }
    public Client? Client { get; set; }
    public Car? Car { get; set; }
    public double TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool Status { get; set; }
}