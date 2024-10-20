namespace AutoManagerAPI.dto;

public class ClientDetailDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public List<CarDto> Cars { get; set; }
    public List<OrderDto> Orders { get; set; }
}