namespace AutoManagerAPI.dto;
using AutoManagerAPI.Models;

public class OrderNoCarDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CarServiceId { get; set; }
    public double TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Status { get; set; }
}