namespace AutoManagerAPI.dto;
using AutoManagerAPI.Models;

public class OrderDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ClientId { get; set;}
    public ClientDto Client { get; set; }
    public string CarId { get; set; }
    public CarDto Car { get; set; }
    public string CarServiceId { get; set; }
    public CarService CarService { get; set; }
    public double TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Status { get; set; }
}