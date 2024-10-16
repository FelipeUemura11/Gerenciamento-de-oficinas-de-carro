namespace AutoManagerAPI.Models;

public class Client 
{
    public Client() 
    {
        Id = Guid.NewGuid().ToString();
    }

    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set;}
}