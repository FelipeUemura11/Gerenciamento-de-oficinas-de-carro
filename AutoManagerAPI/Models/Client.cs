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
    public string? Cpf { get; set;}
    public string? Cep { get; set;}
    public string? Estado { get; set;}
    public string? Cidade { get; set;}
    public string? Bairro { get; set;}
    public string? Logradouro { get; set;}
    public string Senha { get; set;}
    public List<Order> Orders { get; set; }
    public List<Car> Cars { get; set; }
}