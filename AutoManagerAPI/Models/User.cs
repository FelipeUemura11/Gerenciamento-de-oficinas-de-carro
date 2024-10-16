namespace AutoManagerAPI.Models;

public class User 
{
    public User() 
    {
        Id = Guid.NewGuid().ToString();
    }

    public string? Id { get; set; }
    public string? Username { get; set; }
    public Client Client { get; set; }
    public string? Password { get; set; }
}